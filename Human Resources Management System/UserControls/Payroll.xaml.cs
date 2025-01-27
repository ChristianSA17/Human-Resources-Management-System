using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using SharpVectors.Dom;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.Web.WebView2.Core;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Payroll.xaml
    /// </summary>
    
    public partial class Payroll : UserControl
    {

        public ObservableCollection<Payslip> Payslips { get; set; } = new ObservableCollection<Payslip>();
        public Payroll()
        {
            InitializeComponent();
            DataContext = this; // Ensures bindings are recognized
            ListViewUsers.ItemsSource = Payslips; // Bind payslips to the ListView

        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        public void AddPayslip(Payslip payslip)
        {
            if (payslip != null)
            {
                Payslips.Add(payslip);

                // Update Total Employee Count and Total Payroll
                Total_Employee1.Text = Payslips.Count.ToString();
                Total_Payroll1.Text = $"{Payslips.Sum(p => p.BasicSalary + p.Allowances - p.Deductions):C}";
            }
        }


        private void GeneratePayslipButton_Click(object sender, RoutedEventArgs e)
        {
            if (Payslips.Count == 0)
            {
                MessageBox.Show("No payslips available to generate.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Payslip payslip = Payslips[0];


            // Display payslip details in a MessageBox or new window
            string payslipDetails = $"Employee Name: {payslip.EmployeeName}\n" +
                                    $"Employee ID: {payslip.EmployeeId}\n" +
                                    $"Basic Salary: {payslip.BasicSalary:C}\n" +
                                    $"Allowances: {payslip.Allowances:C}\n" +
                                    $"Deductions: {payslip.Deductions:C}\n" +
                                    $"Net Pay: {payslip.NetPay:C}\n" +
                                    $"Pay Date: {payslip.PayDate:d}";

            MessageBox.Show(payslipDetails, "Generated Payslip", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportPayslipButton_Click(object sender, RoutedEventArgs e)
        {
            if (Payslips.Count == 0)
            {
                MessageBox.Show("No payslips available to export.");
                return;
            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = "Payslip.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                GeneratePayslipPDF(Payslips[0], filePath);
                MessageBox.Show($"Payslip exported to {filePath}");
            }
        }

        private void GeneratePayslipPDF(Payslip payslip, string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Salary Slip";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Fonts
            XFont titleFont = new XFont("Arial", 24);
            XFont headerFont = new XFont("Arial", 12);
            XFont contentFont = new XFont("Arial", 10);

            // Title
            gfx.DrawString("SALARY SLIP", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, 40), XStringFormats.Center);

            // Employee Details
            gfx.DrawString($"Name: {payslip.EmployeeName}", contentFont, XBrushes.Black, new XPoint(40, 60));
            gfx.DrawString($"Employee ID: {payslip.EmployeeId:D5}", contentFont, XBrushes.Black, new XPoint(400, 60));
            gfx.DrawString($"Pay Date: {payslip.PayDate:MMMM dd, yyyy}", contentFont, XBrushes.Black, new XPoint(40, 80));

            // Table Header for First Table
            double xStart = 40;
            double yStart = 120;
            double rowHeight = 25;
            double col1Width = 200;
            double col2Width = 100;
            double col3Width = 100;

            // Draw line above header
            double tableWidth = col1Width + col2Width + col3Width;
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Draw Header
            gfx.DrawString("Description", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Earnings", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Deductions", headerFont, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Draw horizontal line under header
            gfx.DrawLine(XPens.Black, xStart, yStart + rowHeight, xStart + tableWidth, yStart + rowHeight);

            yStart += rowHeight;

            // First Table Content
            DrawTableRow(gfx, "Daily Rate", "300", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Hourly Rate", "24.375", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Monthly Rate", "8,775.00", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Overtime", "60.70", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Tax", "", "80.21", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Total", "8,755.49", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);

            /*
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart); // Line above total row
            gfx.DrawString("Total", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width + col2Width, rowHeight, true), XStringFormats.Center); // Total label
            gfx.DrawString("8,755.49", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width + col3Width, rowHeight, true), XStringFormats.CenterLeft); */

            // Draw Vertical Lines
            gfx.DrawLine(XPens.Black, xStart, 120, xStart, yStart); // First vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width, 120, xStart + col1Width, yStart); // Second vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width + col2Width, 120, xStart + col1Width + col2Width, yStart); // Third vertical line
            gfx.DrawLine(XPens.Black, xStart + tableWidth, 120, xStart + tableWidth, yStart); // Last vertical line

            // Draw Final Horizontal Line Below Total Row
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Add spacing before Second Table
            yStart += 40;

            // Second Table Header
            double secondTableStart = yStart;
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart); // Horizontal line above the header row
            gfx.DrawString("Description", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Earnings", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Deductions", headerFont, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Draw horizontal line under header
            gfx.DrawLine(XPens.Black, xStart, yStart + rowHeight, xStart + tableWidth, yStart + rowHeight);

            yStart += rowHeight;

            // Second Table Content (Dynamic)
            DrawTableRow(gfx, "Basic Salary", payslip.BasicSalary.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Allowances", payslip.Allowances.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Deductions", "", payslip.Deductions.ToString("C"), xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Net Pay", payslip.NetPay.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);


            // Draw vertical lines for Second Table (adjusted length)
            gfx.DrawLine(XPens.Black, xStart, secondTableStart, xStart, yStart); // First vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width, secondTableStart, xStart + col1Width, yStart); // Second vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width + col2Width, secondTableStart, xStart + col1Width + col2Width, yStart); // Third vertical line
            gfx.DrawLine(XPens.Black, xStart + tableWidth, secondTableStart, xStart + tableWidth, yStart); // Last vertical line

            // Draw final horizontal line at the bottom of the second table
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Save PDF
            document.Save(filePath);
        }



        // Helper method to draw a table row
        private void DrawTableRow(XGraphics gfx, string description, string earnings, string deductions, double xStart, ref double yStart, double col1Width, double col2Width, double col3Width, XFont font, double rowHeight, bool centerDescription)
        {
            // Draw row content
            if (centerDescription)
            {
                gfx.DrawString(description, font, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            }
            else
            {
                gfx.DrawString(description, font, XBrushes.Black, new XPoint(xStart + 10, yStart + 15)); // Default left-aligned
            }

            gfx.DrawString(earnings, font, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString(deductions, font, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Move to the next row
            yStart += rowHeight;

            // Draw horizontal line for the row
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + col1Width + col2Width + col3Width, yStart);
        }

        private void ViewPayslipButton_Click(object sender, RoutedEventArgs e)
        {
            // Pass the path of the generated PDF to the viewer
            string pdfPath = @"C:\Users\ChristianSA\Desktop\Payslip.pdf";
            if (!File.Exists(pdfPath))
            {
                MessageBox.Show("Payslip file not found!");
                return;
            }

            PayslipViewer viewer = new PayslipViewer(pdfPath);
            viewer.ShowDialog();
        }

        private void OpenPayrollInput_Click(object sender, RoutedEventArgs e)
        {
            var payrollInputWindow = new PayrollInput(this); // Pass the current Payroll instance
            payrollInputWindow.ShowDialog();
        }

        private void ToggleSearchBar(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Visibility == Visibility.Collapsed)
            {
                SearchBar.Visibility = Visibility.Visible;
                SearchBar.Focus();
            }
            else
            {
                SearchBar.Visibility = Visibility.Collapsed;
                SearchBar.Text = string.Empty; // Clear search when hiding

                // Reset the ListView to show all items
                ListViewUsers.Items.Filter = null;
            }
        }
        private void SearchBar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = SearchBar.Text.ToLower();

            // Apply filtering to the ListView
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewUsers.ItemsSource);
            if (!string.IsNullOrEmpty(searchText))
            {
                view.Filter = item => item.ToString().ToLower().Contains(searchText);
            }
            else
            {
                view.Filter = null; // Reset the filter when the search box is empty
            }
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }


    public class Payslip
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay => BasicSalary + Allowances - Deductions;
        public DateTime PayDate { get; set; }
    }
}
