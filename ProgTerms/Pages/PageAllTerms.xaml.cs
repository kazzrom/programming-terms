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
using System.Windows.Threading;

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTerm.xaml
    /// </summary>
    public partial class PageAllTerms : Page
    {
        public PageAllTerms()
        {
            InitializeComponent();

            StkNoneTerms.Visibility = Visibility.Visible;
            TblNoneTerms.Text = "Загрузка...";

            ListAllTerm.ItemsSource = ConnectDB.ProgTermsContext.Terms.ToList();
            ListAllTerm.SelectedIndex = 0;
            SelectTerm();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += UpdateData;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Start();
        }

        private void UpdateData(object? sender, EventArgs e)
        {
            var historyContext = ConnectDB.ProgTermsContext.Terms.ToList();

            BtnNoneTerms.Visibility= Visibility.Collapsed;
            TblNoneTerms.Text = "Загрузка...";

            if(historyContext.Count == 0)
            {
                StkNoneTerms.Visibility = Visibility.Visible;
                TblNoneTerms.Text = "Список терминов отсутствует... Добавьте новый термин!";
                BtnNoneTerms.Visibility = Visibility.Visible;
                GridAllTerm.Visibility = Visibility.Hidden;
            }
            else
            {
                GridAllTerm.Visibility = Visibility.Visible;
                StkNoneTerms.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(WTBSearch.Text))
                ListAllTerm.ItemsSource = historyContext;
            else
                ListAllTerm.ItemsSource = historyContext.Where(term => term.Title.StartsWith(WTBSearch.Text)).ToList();
        }

        private void SelectTerm()
        {
            var selectTerm = ListAllTerm.SelectedItem as Term;
            if (selectTerm != null)
            {
                CurrentTerm.Term = selectTerm;
                TblTitle.Text = selectTerm.Title + " -";
                TblDefinion.Text = selectTerm.Definition;
                BtnBookmark.IsChecked = selectTerm.IsSave == 1;

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
            if (ListAllTerm.SelectedIndex != -1)
            {
                SelectTerm();
                ScrSelectedTerm.Visibility = Visibility.Visible;
                TblNoSelectTerm.Visibility = Visibility.Collapsed;
                MenuButtons.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddTerm_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.FrameMain.Navigate(new PageAddTerm());
            MainObjects.IsMain = false;

        }

        private void BtnEditTerm_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.FrameMain.Navigate(new PageEditTerm(false));
            MainObjects.BtnBack.Visibility = Visibility.Collapsed;
        }

        private void BtnDeleteTerm_Click(object sender, RoutedEventArgs e)
        {
            var MsgBxIsDelete = MessageBox.Show("Вы действительно хотите удалить этот термин?", "Удаление термина", 
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (MsgBxIsDelete == MessageBoxResult.Yes)
            {
                ScrSelectedTerm.Visibility = Visibility.Collapsed;
                TblNoSelectTerm.Visibility = Visibility.Visible;
                MenuButtons.Visibility = Visibility.Hidden;

                ConnectDB.ProgTermsContext.Terms.Remove(CurrentTerm.Term);
                ConnectDB.ProgTermsContext.SaveChanges();
            }
        }

        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            CurrentTerm.Term.IsSave = (bool)BtnBookmark.IsChecked! ? 1 : 0;
            ConnectDB.ProgTermsContext.SaveChanges();
        }
    }
}
