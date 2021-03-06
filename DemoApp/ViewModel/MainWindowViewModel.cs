﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// 응용 프로그램의 Main Window를 위한 ViewModel
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        ReadOnlyCollection<CommandViewModel> _commands;
        readonly CustomerRepository _customerRepository;
        ObservableCollection<WorkspaceViewModel> _workspaces;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel( string customerDataFile )
        {
            base.DisplayName = Strings.MainWindowViewModel_DisplayName;
            _customerRepository = new CustomerRepository( customerDataFile );
        }

        #endregion // Constructor

        #region Commands

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if( _commands == null )
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>( cmds );
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    Strings.MainWindowViewModel_Command_ViewAllCustomers,
                    new RelayCommand(param => ShowAllCustomers())),

                new CommandViewModel(
                    Strings.MainWindowViewModel_Command_CreateNewCustomer,
                    new RelayCommand(param => CreateNewCustomer()))
            };
        }

        #endregion // Commands

        #region Workspaces

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if( _workspaces == null )
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( e.NewItems != null && e.NewItems.Count != 0 )
                foreach( WorkspaceViewModel workspaces in e.NewItems )
                    workspaces.RequestClose += this.OnWorkspaceRequestClose;


            if( e.OldItems != null && e.OldItems.Count != 0 )
                foreach( WorkspaceViewModel workspaces in e.OldItems )
                    workspaces.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose( object sender, EventArgs e )
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove( workspace );
        }

        #endregion // Workspaces

        #region Private Helpers

        private void CreateNewCustomer()
        {
            Customer newCustomer = Customer.CreateNewCustomer();
            CustomerViewModel workspace = new CustomerViewModel( newCustomer, _customerRepository );
            Workspaces.Add( workspace );
            SetActiveWorkspace( workspace );
        }

        private void ShowAllCustomers()
        {
            AllCustomersViewModel workspace =
                this.Workspaces.FirstOrDefault( vm => vm is AllCustomersViewModel )
                as AllCustomersViewModel;

            if( workspace == null )
            {
                workspace = new AllCustomersViewModel( _customerRepository );
                this.Workspaces.Add( workspace );
            }

            this.SetActiveWorkspace( workspace );
        }

        void SetActiveWorkspace( WorkspaceViewModel workspace )
        {
            Debug.Assert( this.Workspaces.Contains( workspace ) );

            ICollectionView collectionView = CollectionViewSource.GetDefaultView( this.Workspaces );
            if( collectionView != null )
                collectionView.MoveCurrentTo( workspace );
        }

        #endregion // Private Helpers 
    }
}
