using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.Interfaces;
using GOLD.CustomerDomain.MVC.UserExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GOLD.CustomerDomain.MVC.LogicalUnits
{
    [ComponentInterface(typeof(ILuPreviewCustomerIncSearchAndSelect))]
    [ComponentTitle("Preview Customer (inc search & select)")]
    [ComponentDescription("Shows the personal details of a specific customer")]
    [ComponentPrimaryRoute("Customer/LuPreviewCustomerIncSearchAndSelect")]
    [ComponentSecondaryRoute("Customer/LuPreviewCustomerIncSearchAndSelect")]
    [ComponentSearchTags("Preview", "Customer", "another tag", "and another tage")]
    [ComponentDependsUpon(typeof(UxPreviewCustomer))]
    public class LuPreviewCustomerIncSearchAndSelect : LogicalUnit, ILuPreviewCustomerIncSearchAndSelect
    {

        [PropertyIsComponentState]
        internal string NextStep { get; set; } = "Criteria";

        [PropertyIsComponentState]
        internal int? SearchTaskId { get; set; } = null;

        [PropertyIsLaunchInput("Customer ID", "int")]
        [PropertyIsComponentState]
        public EntityContext SelectedCustomerContext { get; set; }

        [PropertyIsComponentState]
        internal EntityContext PreviewedCustomerContext { get; set; }

        private UxPreviewCustomer _UxPreviewCustomer;
        private UxCustomerSearchCriteria _UxCustomerSearchCriteria;
        private UxSelectCustomer _UxSelectCustomer;

        private async Task<UxPreviewCustomer> PreviewCustomer()
        {
            if (_UxPreviewCustomer != null) return _UxPreviewCustomer;

            _UxPreviewCustomer = await UseComponentAsync<UxPreviewCustomer>("Preview");
            _UxPreviewCustomer.DoneButtonText = "Finish >";
            _UxPreviewCustomer.ShowBackButton = true;
            _UxPreviewCustomer.BackButtonText = "< Prev";
            _UxPreviewCustomer.CustomerContext = SelectedCustomerContext;
            return _UxPreviewCustomer;
        }
        private async Task<UxCustomerSearchCriteria> CustomerSearchCriteria()
        {
            if (_UxCustomerSearchCriteria != null) return _UxCustomerSearchCriteria;

            _UxCustomerSearchCriteria = await UseComponentAsync<UxCustomerSearchCriteria>("Criteria");
            _UxCustomerSearchCriteria.SearchButtonText = "Next >";
            _UxCustomerSearchCriteria.BackButtonText = "< Prev";
            return _UxCustomerSearchCriteria;
        }

        private async Task<UxSelectCustomer> SelectCustomer()
        {
            if (_UxSelectCustomer != null) return _UxSelectCustomer;

            _UxSelectCustomer = await UseComponentAsync<UxSelectCustomer>("Select");
            _UxSelectCustomer.SelectButtonText = "Next >";
            _UxSelectCustomer.BackButtonText = "< Prev";
            return _UxSelectCustomer;
        }


        public async override Task<IComponent> GetNextComponentAsync()
        {
            if (PreviewedCustomerContext != null) return null;
            if (SelectedCustomerContext != null) NextStep = "Preview";

            if (NextStep == "Criteria")
            {
                return await CustomerSearchCriteria();
            }

            if (NextStep == "Select")
            {
                var uxSelectCustomer = await SelectCustomer();
                uxSelectCustomer.SearchTaskId = SearchTaskId;
                return uxSelectCustomer;
            }

            if (NextStep == "Preview")
            {
                return await PreviewCustomer();
            }

            return null;
        }

        public override void HandleOutcome(Outcome outcome)
        {
            if (outcome is ComponentDoneOutcome)
            {
                if (outcome.SourceComponent as UxCustomerSearchCriteria != null)
                {
                    SearchTaskId = ((UxCustomerSearchCriteria)(outcome.SourceComponent)).TXID.xid;
                    NextStep = "Select";
                    return;
                }

                if (outcome.SourceComponent as UxSelectCustomer != null)
                {
                    SelectedCustomerContext =((UxSelectCustomer)(outcome.SourceComponent)).SelectedCustomerContext;
                    NextStep = "Preview";
                    return;
                }

                if (outcome.SourceComponent as UxPreviewCustomer != null)
                {
                    PreviewedCustomerContext =((UxPreviewCustomer)(outcome.SourceComponent)).CustomerContext;
                    return;
                }

            }
            if (outcome is ComponentBackOutcome)
            {
                if (outcome.SourceComponent as UxCustomerSearchCriteria != null)
                {
                    SearchTaskId = null;
                    NextStep = null;
                    return;
                }

                if (outcome.SourceComponent as UxSelectCustomer != null)
                {
                    SelectedCustomerContext = ((UxSelectCustomer)(outcome.SourceComponent)).SelectedCustomerContext;
                    NextStep = "Criteria";
                    return;
                }

                if (outcome.SourceComponent as UxPreviewCustomer != null)
                {
                    SelectedCustomerContext = null;
                    NextStep = "Select";
                }

            }
        }

    }
}