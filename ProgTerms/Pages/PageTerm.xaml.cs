﻿using ProgTerms.AppData;
using ProgTerms.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTerm.xaml
    /// </summary>
    public partial class PageTerm : Page
    {
        public PageTerm()
        {
            InitializeComponent();
            ListAllTerm.ItemsSource = ConnectDB.ProgTermsContext.Terms.ToList();
            ListAllTerm.SelectedIndex = 0;
            SelectTerm();
        }

        private void SelectTerm()
        {
            var selectTerm = ListAllTerm.SelectedItem as Term;
            if (selectTerm != null)
            {
                CurrentTerm.Term = selectTerm;
                TblTitle.Text = selectTerm.Title + " -";
                TblDefinion.Text = selectTerm.Definition;
                BtnBookmark.IsChecked = selectTerm.IsSave;

                if (!string.IsNullOrEmpty(selectTerm.AddInformation))
                {
                    StkAddInfo.Visibility = Visibility.Visible;
                    TblAddInfo.Text = selectTerm.AddInformation;
                }
                else
                    StkAddInfo.Visibility = Visibility.Collapsed;
            }
        }

        private void ListAllTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectTerm();
        }

        private void BtnAddTerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditTerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteTerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}