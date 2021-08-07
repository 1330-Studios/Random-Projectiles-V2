
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Unity;
using Harmony;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnhollowerRuntimeLib;

namespace Random_Projectiles_V2 {
    partial class Controller {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Controller));
            this.currentSeedLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.setSeedButton = new System.Windows.Forms.Button();
            this.Randomize = new System.Windows.Forms.Button();
            this.experimental = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // currentSeedLabel
            // 
            this.currentSeedLabel.AutoSize = true;
            this.currentSeedLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSeedLabel.Location = new System.Drawing.Point(13, 80);
            this.currentSeedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentSeedLabel.Name = "currentSeedLabel";
            this.currentSeedLabel.Size = new System.Drawing.Size(48, 13);
            this.currentSeedLabel.TabIndex = 0;
            this.currentSeedLabel.Text = $"Current Seed: {URandom.lastRand}";
            this.currentSeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(12, 35);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(414, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.WordWrap = false;
            // 
            // setSeedButton
            // 
            this.setSeedButton.Location = new System.Drawing.Point(434, 35);
            this.setSeedButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.setSeedButton.Name = "setSeedButton";
            this.setSeedButton.Size = new System.Drawing.Size(104, 22);
            this.setSeedButton.TabIndex = 2;
            this.setSeedButton.Text = "Set";
            this.setSeedButton.UseVisualStyleBackColor = true;
            this.setSeedButton.Click += new System.EventHandler(this.setSeed);
            // 
            // Randomize
            // 
            this.Randomize.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.Randomize.Location = new System.Drawing.Point(283, 63);
            this.Randomize.Name = "Randomize";
            this.Randomize.Size = new System.Drawing.Size(255, 32);
            this.Randomize.TabIndex = 3;
            this.Randomize.Text = "Start Randomizing";
            this.Randomize.UseVisualStyleBackColor = true;
            this.Randomize.Click += new System.EventHandler(this.startRandomizing);
            // 
            // experimental
            // 
            this.experimental.AutoSize = true;
            this.experimental.Location = new System.Drawing.Point(12, 12);
            this.experimental.Name = "experimental";
            this.experimental.Size = new System.Drawing.Size(348, 17);
            this.experimental.TabIndex = 4;
            this.experimental.Text = "Experimental Mode (Enables more towers but has more bugs)";
            this.experimental.UseVisualStyleBackColor = true;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 102);
            this.Controls.Add(this.experimental);
            this.Controls.Add(this.Randomize);
            this.Controls.Add(this.setSeedButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.currentSeedLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Controller";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Random Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label currentSeedLabel;
        private TextBox textBox1;
        private Button setSeedButton;
        private Button Randomize;

        private bool Randomized = false;
        private readonly List<(EmissionModel, ProjectileModel)> items = new List<(EmissionModel, ProjectileModel)>();

        private void setSeed(object sender, EventArgs a) {
            try {
                var u = ulong.Parse(textBox1.Text);
                URandom.lastRand = u;
                currentSeedLabel.Text = $"Current Seed: {URandom.lastRand}";
            } catch (Exception) {
                MessageBox.Show("Invalid Seed!");
            }
        }

        private void CurrentSeedLabel_EnabledChanged(object sender, EventArgs e) {
            currentSeedLabel.Text = $"Current Seed: {URandom.lastRand}";
        }

        private void startRandomizing(object sender, EventArgs e) {
            textBox1.Enabled = false;
            setSeedButton.Enabled = false;
            Randomize.Enabled = false;
            experimental.Enabled = false;

            var model = Game.instance.model;
            var seed = URandom.lastRand;

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            if (!Randomized) {
                if (experimental.Checked) {

                    for (var i = 0; i < model.towers.Length; i++)
                        for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                            if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>() || model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackAirUnitModel>())
                                for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                    if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null)
                                        items.Add((model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].emission, model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile));

                    for (var i = 0; i < model.towers.Length; i++)
                        for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                            if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>() || model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackAirUnitModel>())
                                for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                    if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null) {
                                        var p = items.GetRandomObjectFromList();
                                        model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].emission = p.Item1;
                                        model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile = p.Item2;
                                    }
                } else {
                    for (var i = 0; i < model.towers.Length; i++)
                        if (!model.towers[i].IsHero() && model.towers[i].tier == 5 && !model.towers[i].name.Contains("Alchemist") && !model.towers[i].name.Contains("Wizard") && !(model.towers[i].name.Contains("Dartling") && !model.towers[i].name.Contains("Engineer") && (model.towers[i].tiers[0] == 4 || model.towers[i].tiers[0] == 5)))
                            for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                                if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>())
                                    for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                        if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null)
                                            items.Add((null, model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile));

                    for (var i = 0; i < model.towers.Length; i++)
                        if (!model.towers[i].name.Contains("Alchemist") && !model.towers[i].name.Contains("Wizard") && !(model.towers[i].name.Contains("Dartling") && !model.towers[i].name.Contains("Engineer") && (model.towers[i].tiers[0] == 4 || model.towers[i].tiers[0] == 5)))
                            for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                                if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>())
                                    for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                        if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null)
                                            model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile = items.GetRandomObjectFromList().Item2;
                }

                MessageBox.Show($"Randomized projectiles with seed: {seed}");
                Randomized = true;
                //this.Randomize.Text = "Start UnRandomizing";
            } else {
                if (experimental.Checked) {
                    var index = 0;
                    for (var i = 0; i < model.towers.Length; i++)
                        for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                            if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>() || model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackAirUnitModel>())
                                for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                    if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null) {
                                        var p = items[index];
                                        model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].emission = p.Item1;
                                        model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile = p.Item2;
                                        index++;
                                    }
                } else {
                    var index = 0;
                    for (var i = 0; i < model.towers.Length; i++)
                        if (!model.towers[i].IsHero() && model.towers[i].tier == 5 && !model.towers[i].name.Contains("Alchemist") && !model.towers[i].name.Contains("Wizard") && !(model.towers[i].name.Contains("Dartling") && !model.towers[i].name.Contains("Engineer") && (model.towers[i].tiers[0] == 4 || model.towers[i].tiers[0] == 5)))
                            for (var j = 0; j < model.towers[i].behaviors.Length; j++)
                                if (model.towers[i].behaviors[j].GetIl2CppType() == Il2CppType.Of<AttackModel>())
                                    for (var k = 0; k < model.towers[i].behaviors[j].Cast<AttackModel>().weapons.Length; k++)
                                        if (model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile != null) {
                                            model.towers[i].behaviors[j].Cast<AttackModel>().weapons[k].projectile = items[index].Item2;
                                            index++;
                                            MelonLoader.MelonLogger.Msg($"{index}   {items.Count}");
                                        }
                }


                MessageBox.Show($"UnRandomized projectiles back to their defaults");
                //Randomized = false;
                this.Randomize.Text = "Start Randomizing";
            }

            Randomize.Enabled = true;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private CheckBox experimental;
    }
}