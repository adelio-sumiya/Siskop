﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siskop.Models;
using Models;

namespace Siskop.Views
{
    public partial class karyawanDetails : UserControl
    {
        private readonly KaryawanModel _karyawanModel;
        private readonly MainForm _mainForm;
        private Karyawan _currentKaryawan;

        public karyawanDetails()
        {
            InitializeComponent();
            InitializeGenderComboBox();
        }

        public karyawanDetails(MainForm a,KaryawanModel x, Karyawan karyawan) : this()
        {
            _mainForm = a;
            _karyawanModel = x;
            _currentKaryawan = karyawan;
            PopulateFields();
        }

        private void InitializeGenderComboBox()
        {
            cbKelamin.Items.Clear();
            cbKelamin.Items.Add("Laki-laki");
            cbKelamin.Items.Add("Perempuan");
        }
        private void InitializeGRolerComboBox()
        {
            cbRole.Items.Clear();
            cbRole.Items.Add("Admin");
            cbRole.Items.Add("Perempuan");
        }


        private void PopulateFields()
        {
            if (_currentKaryawan == null) return;

            tbNama.Text = _currentKaryawan.Nama_Karyawan ?? "";
            tbjabatan.Text = _currentKaryawan.Jabatan ?? "";
            tbAlamat.Text = _currentKaryawan.Alamat ?? "";
            tbKontak.Text = _currentKaryawan.Kontak ?? "";
            dtpTanggalLahir.Value = _currentKaryawan.Tanggal_Lahir;
            cbKelamin.Text = _currentKaryawan.Jenis_Kelamin ?? "";
            tbUsername.Text = _currentKaryawan.Username ?? "";
            tbPassword.Text = _currentKaryawan.Password ?? "";
            cbRole.Text = _currentKaryawan.Role ?? "";
        }


        public async Task<bool> SaveKaryawan()
        {
            if (_karyawanModel == null) return false;

            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(tbNama.Text))
                {
                    MessageBox.Show("Nama is required", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(tbjabatan.Text))
                {
                    MessageBox.Show("Jabatan is required", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (cbKelamin.SelectedIndex == -1)
                {
                    MessageBox.Show("Jenis Kelamin is required", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                else
                {
                    // Update existing karyawan
                    await _karyawanModel.UpdateKaryawan(
                        _currentKaryawan.ID_Karyawan,
                        tbNama.Text.Trim(),
                        tbjabatan.Text.Trim(),
                        dtpTanggalLahir.Value,
                        tbAlamat.Text.Trim(),
                        cbKelamin.Text,
                        tbKontak.Text.Trim(),
                        tbUsername.Text.Trim(),
                        tbPassword.Text.Trim(),
                        cbRole.Text

                    );

                    MessageBox.Show("Karyawan updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving karyawan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var success = await SaveKaryawan();
            if (success)
            {
                _mainForm.ShowPage(_mainForm.adminKaryawan);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Any unsaved changes will be lost.",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _mainForm.ShowPage(_mainForm.adminKaryawan);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}