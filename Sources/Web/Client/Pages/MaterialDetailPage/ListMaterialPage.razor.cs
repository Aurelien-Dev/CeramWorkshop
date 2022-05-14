﻿using Client.Utils;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.MaterialDetailPage
{
    public partial class ListMaterialPage : PageComponentBase
    {
        [Inject] private IMaterialRepository MaterialRepository { get; set; } = default!;

        public IEnumerable<Material> Materials { get; set; } = new List<Material>();

        public Material MaterialForm { get; set; } = default!;

        public bool test { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Materials = await MaterialRepository.GetAll();
        }

        #region Material editing


        public void SaveMaterialEventHandler(EditContext context)
        {
            bool isValid = context.Validate();
            if (isValid)
            {

            }
        }

        #endregion
    }
}