// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Linq;
using System.Collections.Generic;

namespace Doc.NotNull
{
    internal class Invoice
    {
        private List<InvoiceLine> _lines = new();

        [NormalizeId]
        public string InvoiceId { get; set; }

        public Invoice( string invoiceId )
        {
            this.InvoiceId = invoiceId;
        }

        public InvoiceLine GetLine( [NormalizeId] string id )
            => _lines.Single( x => x.LineId == id );
    }

    internal class InvoiceLine
    {
        [NormalizeId]
        public string LineId { get; set; }
    }
}