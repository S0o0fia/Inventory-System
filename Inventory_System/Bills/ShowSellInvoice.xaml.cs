﻿using Final;
using Inventory_System.Adding;
using Inventory_System.Connects;
using Inventory_System.EF_Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Inventory_System.Bills
{
    /// <summary>
    /// Interaction logic for ShowInvoice.xaml
    /// </summary>
    public partial class ShowInvoice : Window
    {
        Context context;
        IQueryable<SalesInvoice> query;
        public ShowInvoice()
        {
            InitializeComponent();
            context = new Context();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Invoice.IsEnabled = true;
            List.ItemsSource = null;
             Picker.IsEnabled = false;
            NameTextBox.IsEnabled = true;
            GetInvoice.IsEnabled = false;
              KindOfinvoice.IsEnabled = false;
          
        }

        private void DateRadio_Checked(object sender, RoutedEventArgs e)
        {
            Invoice.IsEnabled = true;
            List.ItemsSource = null;
            Picker.IsEnabled = true;
            GetInvoice.IsEnabled = false;
            NameTextBox.IsEnabled = false;
            KindOfinvoice.IsEnabled = false;
          
    
     
        }

        private void SpecialInvoice_Checked(object sender, RoutedEventArgs e)
        {
            KindOfinvoice.IsEnabled = true;
            List.ItemsSource = null;
            Invoice.IsEnabled = false;
            KindOfinvoice.IsEnabled = true;
            Picker.IsEnabled = true;
            GetInvoice.IsEnabled = true;
            NameTextBox.IsEnabled = true;
          
        }

        private void NameTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerRadio.IsChecked==true)
            {
                int Cus_Id = int.Parse(NameTextBox.SelectedValue.ToString());
                query = context.SalesInvoices.Where(c => c.salesman_Id == Cus_Id);
               
                Invoice.SelectedValuePath = "ID";
                Invoice.DisplayMemberPath = "ID";
                Invoice.ItemsSource = query.ToList();
                var qq = query.ToList();
                if (qq.Count == 0)
                {
                    MessageBox.Show("No Invoice With This Data");
                } 
               

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var Customers = context.salesmans;
            NameTextBox.SelectedValuePath = "ID";
            NameTextBox.DisplayMemberPath = "Name";
            NameTextBox.ItemsSource = Customers.ToList();

        }

        private void Button_Click10(object sender, RoutedEventArgs e)
        {
            AddCategory add = new AddCategory();
            add.Show();
            this.Close();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            AddItem add = new AddItem();
            add.Show();
            this.Close();

        }



        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            Billls bill = new Billls();
            bill.Show();
            this.Close();

        }

        private void Button_Click_40(object sender, RoutedEventArgs e)
        {
            AddSalesMan add = new AddSalesMan();
            add.Show();
            this.Close();

        }

        private void Button_Click_50(object sender, RoutedEventArgs e)
        {
            AddSupplier add = new AddSupplier();
            add.Show();
            this.Close();

        }

        private void Button_Click_60(object sender, RoutedEventArgs e)
        {
            Reportss report = new Reportss();
            report.Show();
            this.Close();
        }

        private void Button_Click_70(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();

        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            AddCustomer add = new AddCustomer();
            add.Show();
            this.Close();

        }
        private void Invoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NoInvoice.IsChecked==true)
            {
                query = context.SalesInvoices;
            }
            if (Invoice.SelectedValue == null)
                return;
            int Inv_Id= int.Parse(Invoice.SelectedValue.ToString());
            var first = query.Where(t=>t.ID==Inv_Id).FirstOrDefault();
            NameTextBox.Text = context.salesmans.Where(t => t.ID == first.salesman_Id).Select(tt => tt.Name).FirstOrDefault();
            if (first.KindOfInvoice == true)
                KindOfinvoice.Text = "Sell";
            else
                KindOfinvoice.Text = "Back";
            var Items = from p in context.ItemInSalesInvoices
                        from inv in context.SalesInvoices
                        from t in context.Items
                        from cus in context.Customers
                        where p.SalesInvoice_Id == first.ID && p.Item_Id == t.ID && inv.Customer_Id == cus.ID && p.SalesInvoice_Id == inv.ID
                        select new ItemsMapping
                        {
                            Name = t.name,
                            SalesMan = cus.Name,
                            Quantity = p.Quantity,
                            BuyPrice = t.BuyPrice,
                            TotalPrice = p.Quantity * t.BuyPrice,
                            Date = inv.Date
                        };
            if (first == null)
            {
                return;
            }
            List.ItemsSource = Items.ToList();
        }

        private void Picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            List.ItemsSource=null;
            if (DateRadio.IsChecked == true)
            {
                DateTime? date = Picker.SelectedDate;
                 query = context.SalesInvoices.Where(c => c.Date==date);
                Invoice.SelectedValuePath = "ID";
                Invoice.DisplayMemberPath = "ID";
                Invoice.ItemsSource = query.ToList();
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Invoice.IsEnabled = true;
            List.ItemsSource = null;
            KindOfinvoice.IsEnabled = true;
            Picker.IsEnabled = false;
            GetInvoice.IsEnabled = false;
            NameTextBox.IsEnabled = false;
    
        }
        bool InvoiceValue = false;
        private void KindOfinvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ByKind.IsChecked==true)
            {
                ComboBoxItem com = (ComboBoxItem)KindOfinvoice.SelectedItem;
                string value = com.Content.ToString();
                if (value == "Sell")
                    InvoiceValue = true;
                else
                    InvoiceValue = false;
                query = context.SalesInvoices.Where(c => c.KindOfInvoice == InvoiceValue);
                Invoice.SelectedValuePath = "ID";
                Invoice.DisplayMemberPath = "ID";
                Invoice.ItemsSource = query.ToList();
        
            }
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            List.ItemsSource = null;
            Invoice.IsEnabled = true;
            KindOfinvoice.IsEnabled = false;
            Picker.IsEnabled = false;
            GetInvoice.IsEnabled = false;
            NameTextBox.IsEnabled = false;
            var query = context.SalesInvoices;
            Invoice.SelectedValuePath = "ID";
            Invoice.DisplayMemberPath = "ID";
            Invoice.ItemsSource = query.ToList();
        
        }
        bool InvoiceType = false;
        private void GetInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            { if (NameTextBox.SelectedIndex!=-1&&KindOfinvoice.SelectedIndex!=-1&&Picker.SelectedDate!=null)
            {
                ComboBoxItem com = (ComboBoxItem)KindOfinvoice.SelectedItem;
                string value = com.Content.ToString();
                int name = int.Parse(NameTextBox.SelectedValue.ToString());
                if (value == "Sell")
                    InvoiceType = true;
                else
                    InvoiceType = false;
                DateTime? date = Picker.SelectedDate;
                var query = context.SalesInvoices.Where(c => c.KindOfInvoice == InvoiceType&&c.salesman_Id==name&&c.Date==date).FirstOrDefault();
                var Items = from p in context.ItemInSalesInvoices
                            from inv in context.SalesInvoices
                            from t in context.Items
                            from cus in context.Customers
                            from sal in context.salesmans
                            where p.SalesInvoice_Id == query.ID && p.Item_Id == t.ID && inv.Customer_Id == cus.ID && p.SalesInvoice_Id == inv.ID && inv.KindOfInvoice == InvoiceType && sal.ID == name && inv.Date == date
                            select new ItemsMapping
                            {
                                Name = t.name,
                                SalesMan = cus.Name,
                                Quantity = p.Quantity,
                                BuyPrice = t.BuyPrice,
                                TotalPrice = p.Quantity * t.BuyPrice,
                                Date = inv.Date
                            };
                if (query == null)
                {
                    MessageBox.Show("No Invoice With This Data");
                    return;
                }
                List.ItemsSource = Items.ToList();
               
            }
            else
            {
                MessageBox.Show("Enter All Data");
            }
            }
            catch
            {
                MessageBox.Show("Enter Valid Data");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
