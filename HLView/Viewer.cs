﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLView.Graphics;
using HLView.Graphics.Renderables;
using Veldrid;

namespace HLView
{
    public partial class Viewer : Form
    {
        private GraphicsDevice _graphicsDevice;
        private VeldridControl _view;
        private SceneContext _sc;
        private Scene _scene;

        public Viewer()
        {
            InitializeComponent();
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            var options = new GraphicsDeviceOptions()
            {
                HasMainSwapchain = false,
                ResourceBindingModel = ResourceBindingModel.Improved,
            };
            //_graphicsDevice = GraphicsDevice.CreateVulkan(options);
            _graphicsDevice = GraphicsDevice.CreateD3D11(options);
            _view = new VeldridControl(_graphicsDevice, options)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_view);
            
            _sc = new SceneContext(_graphicsDevice);
            _sc.AddRenderTarget(_view);

            _scene = new Scene(_graphicsDevice);

            _scene.AddRenderable(new SquareRenderable());

            _sc.Scene = _scene;
            _sc.Start();
        }

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sc.RemoveRenderTarget(_view);

            _sc.Stop();
            _sc.Dispose();
            _view.Dispose();
        }
    }
}