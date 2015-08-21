using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ImageResize
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool maintainAspectRatio;
        private int origHeight;
        private int origWidth; 
        private int height;
        private int width;
        private string firstFileName;         

        public VM()
        {                                    
            this.OKCommand = new RelayCommand(this.OK);
            this.CancelCommand = new RelayCommand(this.Cancel);
                 
            string[] args = Environment.GetCommandLineArgs();
            this.SourceFiles = new string[args.Length - 1]; 
            for(int i = 1; i < args.Length; i++)
                this.SourceFiles[i-1] = args[i];
            FileInfo fi = new FileInfo(this.SourceFiles[0]);
            this.FirstFileName = fi.Name;
            Image img = Image.FromFile(this.SourceFiles[0]);
            this.OrigHeight = this.Height = img.Width;
            this.OrigWidth = this.Width = img.Height; 
            this.MaintainAspectRatio = true;
            this.PropertyChanged += VM_PropertyChanged;
        }

        void VM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Width":                                   
                case "MaintainAspectRatio":
                    if (this.MaintainAspectRatio)
                    {
                        this.PropertyChanged -= VM_PropertyChanged;
                        this.UpdateAspectWidth();
                        this.PropertyChanged += VM_PropertyChanged; 
                    }
                    break;

                case "Height":
                    if (this.MaintainAspectRatio)
                    {
                        this.PropertyChanged -= VM_PropertyChanged;
                        this.UpdateAspectHeight();
                        this.PropertyChanged += VM_PropertyChanged; 
                    }                                                        
                    break;
            }
        }

        private void UpdateAspectHeight()
        {
            decimal diff = (decimal)(this.Height) / this.origHeight;            
            this.Width = (int)Math.Ceiling(this.origWidth * diff);
        }

        private void UpdateAspectWidth()
        {
            decimal diff = (decimal)(this.Width) / this.origWidth;                        
            this.Height = (int)Math.Ceiling(this.origHeight * diff);
        }

        public void OK(object p)
        {
            Resizer r = new Resizer
            {
                Width = this.Width,
                Height = this.Height,              
            };
            foreach (string file in this.SourceFiles)
            {
                r.SourceFile = file; 
                r.Convert();
            }
            this.CloseWindow();
        }

        private void CloseWindow()
        {
            this.PropertyChanged -= VM_PropertyChanged;
            Application.Current.MainWindow.Close();
        }

        public void Cancel(object p)
        {
            this.CloseWindow();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler h = this.PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(name));
        }        

        public bool MaintainAspectRatio
        {
            get { return this.maintainAspectRatio; }
            set { if (this.maintainAspectRatio != value) { this.maintainAspectRatio = value; OnPropertyChanged("MaintainAspectRatio"); } }
        }
        
        public int Height
        {
            get { return this.height; }
            set { if (this.height != value) { this.height = value; OnPropertyChanged("Height"); } }
        }        

        public int Width
        {
            get { return this.width; }
            set { if (this.width != value) { this.width = value; OnPropertyChanged("Width"); } }
        }

        public int OrigHeight
        {
            get { return this.origHeight; }
            set { if (this.origHeight != value) { this.origHeight = value; OnPropertyChanged("OrigHeight"); } }
        }

        public int OrigWidth
        {
            get { return this.origWidth; }
            set { if (this.origWidth != value) { this.origWidth = value; OnPropertyChanged("OrigWidth"); } }
        }

        public string[] SourceFiles { get; private set; }
        public RelayCommand OKCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public string FirstFileName { get { return this.firstFileName; } set { this.firstFileName = value; OnPropertyChanged("FirstFileName"); } }
        
    }
}
