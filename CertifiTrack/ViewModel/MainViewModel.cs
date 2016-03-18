﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CertifiTrack.Properties;
using CertifiTrack.Resources;
using Microsoft.Win32;

namespace CertifiTrack.ViewModel {
    internal class MainViewModel {
        public MainViewModel() {
            if (_loaded ||
                (bool)
                    DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty,
                        typeof(DependencyObject)).Metadata.DefaultValue) return;
            _loaded = true;

            LoadCertificates();

            foreach (DeathCertificate cert in ExcelData.GetCertificates(Settings.Default.statusFile)) {
                Certificates.Add(cert);
            }
        }

        private readonly bool _loaded;
        public ObservableCollection<DeathCertificate> Certificates { get; } = new ObservableCollection<DeathCertificate>();
        public string FilePath { get; set; }

        private void LoadCertificates() {
            if (string.IsNullOrEmpty(Settings.Default.statusFile)) {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() != true) return;

                FilePath = open.FileName;
                Settings.Default.statusFile = open.FileName;
                Settings.Default.Save();
            } else {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() != true) return;

                FilePath = open.FileName;
                Settings.Default.statusFile = open.FileName;
                Settings.Default.Save();
            }
        }
    }
}