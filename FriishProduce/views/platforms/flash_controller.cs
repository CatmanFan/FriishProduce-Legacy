﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce.Views
{
    public partial class Flash_Controller : Form
    {
        internal readonly string[] WiiBtns =
        {
            "KEY_BUTTON_LEFT",
            "KEY_BUTTON_RIGHT",
            "KEY_BUTTON_DOWN",
            "KEY_BUTTON_UP",
            "KEY_BUTTON_A",
            "KEY_BUTTON_B",
            "KEY_BUTTON_1",
            "KEY_BUTTON_2",
            "KEY_BUTTON_PLUS",
            "KEY_BUTTON_MINUS",
            "KEY_BUTTON_HOME",
            "KEY_BUTTON_C",
            "KEY_BUTTON_Z",
            "KEY_CL_BUTTON_LEFT",
            "KEY_CL_BUTTON_RIGHT",
            "KEY_CL_BUTTON_DOWN",
            "KEY_CL_BUTTON_UP",
            "KEY_CL_BUTTON_A",
            "KEY_CL_BUTTON_B",
            "KEY_CL_BUTTON_X",
            "KEY_CL_BUTTON_Y",
            "KEY_CL_BUTTON_PLUS",
            "KEY_CL_BUTTON_MINUS",
            "KEY_CL_BUTTON_HOME",
            "KEY_CL_TRIGGER_L",
            "KEY_CL_TRIGGER_R",
            "KEY_CL_TRIGGER_ZL",
            "KEY_CL_TRIGGER_ZR",
        };
        internal string[] SrcBtns
        {
            get
            {
                List<string> List = new List<string>
                {
                    "—",
                    "KEY_LEFT",
                    "KEY_RIGHT",
                    "KEY_DOWN",
                    "KEY_UP",
                    "KEY_ENTER",
                    "KEY_SPACE",
                    "KEY_SHIFT",
                    "KEY_CTRL",
                    "KEY_DELETE",
                    "KEY_ESCAPE",
                    "KEY_BACKSPACE"
                };
                for (int i = 0; i <= 'Z' - 'A'; i++)
                    List.Add($"{(char)(i + 'A')}");
                for (int i = 1; i <= 10; i++)
                    List.Add($"{i - 1}");
                return List.ToArray();
            }
        }
        internal Dictionary<string, string> Config { get; set; }

        public Flash_Controller(Dictionary<string, string> srcConfig)
        {
            InitializeComponent();
            Program.Language.Localize(this);
            Config = srcConfig ?? new Dictionary<string, string>();
        }

        private void Page_Load(object sender, EventArgs e)
        {
            foreach (ComboBox c in WiiRemote.Controls.OfType<ComboBox>())
            {
                c.Items.AddRange(SrcBtns);
                c.SelectedIndex = 0;

                foreach (KeyValuePair<string, string> item in Config)
                    if (c.Tag.ToString() == item.Key)
                        c.SelectedItem = item.Value;
            }
            foreach (ComboBox c in ClassicController.Controls.OfType<ComboBox>())
            {
                c.Items.AddRange(SrcBtns);
                c.SelectedIndex = 0;

                foreach (KeyValuePair<string, string> item in Config)
                    if (c.Tag.ToString() == item.Key)
                        c.SelectedItem = item.Value;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Config = new Dictionary<string, string>();

            foreach (var button in WiiRemote.Controls.OfType<ComboBox>())
            {
                foreach (string btnName in WiiBtns)
                {
                    if (button.Tag.ToString() == btnName && button.SelectedIndex > 0)
                        Config.Add(button.Tag.ToString(), SrcBtns[button.SelectedIndex]);
                }
            }

            foreach (var button in ClassicController.Controls.OfType<ComboBox>())
            {
                foreach (string btnName in WiiBtns)
                {
                    if (button.Tag.ToString() == btnName && button.SelectedIndex > 0)
                        Config.Add(button.Tag.ToString(), SrcBtns[button.SelectedIndex]);
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
