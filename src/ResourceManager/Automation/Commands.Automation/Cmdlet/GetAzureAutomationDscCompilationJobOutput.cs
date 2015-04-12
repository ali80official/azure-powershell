﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Permissions;
using Microsoft.Azure.Commands.Automation.Common;
using Microsoft.Azure.Commands.Automation.Model;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Automation.Cmdlet
{
    /// <summary>
    /// Gets stream for a compilation job
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureAutomationDscCompilationJobOutput")]
    [OutputType(typeof(JobStream))]
    public class GetAzureAutomationDscCompilationJobOutput : AzureAutomationBaseCmdlet
    {
        /// <summary>
        /// Gets or sets the automation account name.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The automation account name.")]
        [ValidateNotNullOrEmpty]
        public string AutomationAccountName { get; set; }

        /// <summary>
        /// Gets or sets the job id
        /// </summary>
        [Alias("JobId")]
        [Parameter(Mandatory = true, Position = 2, ValueFromPipelineByPropertyName = true, HelpMessage = "The job Id")]
        public Guid Id { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "The stream type. Defaults to Any.")]
        public StreamType Stream { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Retrieves output created after this time")]
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// Execute this cmdlet.
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void AutomationExecuteCmdlet()
        {
            var ret = this.AutomationClient.GetDscCompilationJobStream(this.ResourceGroupName, this.AutomationAccountName, this.Id, this.StartTime, this.Stream.ToString());

            this.GenerateCmdletOutput(ret);
        }
    }
}
