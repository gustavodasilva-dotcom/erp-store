using System;
using System.Text;
using System.Configuration;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ERP.Store.Desktop.Services
{
    public class PdfService
    {
        private readonly string _filePath;

        public PdfService()
        {
            _filePath = ConfigurationManager.AppSettings["fileToSavePdf"].ToString();
        }

        public void CreateOrderPdf(dynamic order)
        {
            try
            {
                var document = new Document();

                var page = document.Pages.Add();

                page.Paragraphs.Add(new TextFragment($"OrderID: {order.orderID.ToString()}"));

                InstantializeClientContent(order.client, page);
                InstantializeAddressContent(order.client.address, page);
                InstantializeContactContent(order.client.contact, page);
                InstantializeItemsContent(order.items, page);

                document.Save($"{_filePath}order_{order.orderID.ToString()}_{DateTime.Now.ToString("G").Replace("/", "").Replace(" ", "").Replace(":", "")}.pdf");
            }
            catch (Exception) { throw; }
        }

        private static TextFragment SetFonts(TextSegment segment)
        {
            try
            {
                var fragment = new TextFragment();

                var textState = new TextState
                {
                    Font = FontRepository.FindFont("Arial")
                };

                textState.Font.IsEmbedded = true;

                segment.TextState = textState;
                fragment.Segments.Add(segment);

                return fragment;
            }
            catch (Exception) { throw; }
        }

        private static void InstantializeClientContent(dynamic client, Page page)
        {
            try
            {
                var clientContent = new StringBuilder("Client's info: \n");
                clientContent.Append($"Name: {client.firstName.ToString()} {client.middleName.ToString()} {client.lastName.ToString()} \n");
                clientContent.Append($"Identification: {client.identification.ToString()}");

                var segment = new TextSegment(clientContent.ToString());

                var fragment = SetFonts(segment);

                page.Paragraphs.Add(new TextFragment(""));
                page.Paragraphs.Add(fragment);
            }
            catch (Exception) { throw; }
        }

        private static void InstantializeAddressContent(dynamic address, Page page)
        {
            try
            {
                var addressText = new StringBuilder("Address: ");

                addressText.Append($"{address.street.ToString()}, {address.number.ToString()}");

                if (!string.IsNullOrEmpty(address.comment.ToString())) addressText.Append($", {address.comment.ToString()}");

                addressText.Append($" - {address.neighborhood.ToString()}, {address.city.ToString()} - {address.state.ToString()}");
                addressText.Append($" - {address.country.ToString()}, {address.zip.ToString()}");

                var segment = new TextSegment(addressText.ToString());

                var fragment = SetFonts(segment);

                page.Paragraphs.Add(new TextFragment(""));
                page.Paragraphs.Add(fragment);
            }
            catch (Exception) { throw; }
        }

        private static void InstantializeContactContent(dynamic contact, Page page)
        {
            try
            {
                var contactText = new StringBuilder("Contact: ");

                contactText.Append($"Email: {contact.email.ToString()} \n");
                contactText.Append($"Cellphone: {contact.cellphone.ToString()} \n");
                contactText.Append($"Phone: {contact.phone.ToString()}");

                var segment = new TextSegment(contactText.ToString());

                var fragment = SetFonts(segment);

                page.Paragraphs.Add(new TextFragment(""));
                page.Paragraphs.Add(fragment);
            }
            catch (Exception) { throw; }
        }

        private static void InstantializeItemsContent(dynamic items, Page page)
        {
            try
            {
                page.Paragraphs.Add(new TextFragment(""));

                var itemsTable = new Table
                {
                    Border = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.LightGray)),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.LightGray)),
                };

                itemsTable.DefaultCellPadding = new MarginInfo
                {
                    Left = 10,
                    Right = 10,
                    Top = 10,
                    Bottom = 10
                };

                var row = itemsTable.Rows.Add();
                row.Cells.Add("Item ID: ");
                row.Cells.Add("Quantity: ");
                row.Cells.Add("Category: ");
                row.Cells.Add("Supplier: ");

                foreach (var item in items)
                {
                    row = itemsTable.Rows.Add();
                    row.Cells.Add(item.itemID.ToString());
                    row.Cells.Add(item.quantity.ToString());
                    row.Cells.Add(item.category.description.ToString());
                    row.Cells.Add(item.inventory.supplier.name.ToString());
                }

                page.Paragraphs.Add(itemsTable);
            }
            catch (Exception) { throw; }
        }
    }
}
