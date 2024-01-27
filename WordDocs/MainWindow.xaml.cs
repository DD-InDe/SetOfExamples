using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Path = System.IO.Path;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;

namespace WordDocs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\saveDocs\"));

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            string filePath = path + $"new{DateTime.Now.ToString("MM-dd")}.docx";
            File.Copy(path + "template.docx", filePath, true);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
            {
                var bookmarksStart = doc.MainDocumentPart.Document.Body.Descendants<BookmarkStart>().ToList();
                foreach (var item in bookmarksStart)
                {
                    string input = string.Empty;
                    switch (item.Name.ToString())
                    {
                        case "FirstNameField":
                            input = FirstNameTextBox.Text;
                            break;
                        case "LastNameField":
                            input = LastNameTextBox.Text;
                            break;
                        case "MiddleNameField":
                            input = MiddleNameTextBox.Text;
                            break;
                        case "PhoneField":
                            input = PhoneNameTextBox.Text;
                            break;
                        case "BirthField":
                            input = ((DateTime)DatePicker.SelectedDate).ToString("MM/dd/yyyy");
                            break;
                    }

                    var run = new Run(new Text(input));
                    item.Parent.InsertAfter(run,item);
                }
            }
        }
    }
}