using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnalLibrary.Navigation
{
    public interface IPivotNavigationAware
    {
        /// <summary>
        /// Calls after the navigation takes place.
        /// </summary>
        void OnPivotNavigatedFrom();

        /// <summary>
        /// Calls before the navigation takes place.
        /// </summary>
        void OnPivotNavigatedTo(PivotNavigationContext navigationContext);
    }
}
