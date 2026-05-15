import type { InvoiceLineItem } from "../types/InvoiceLineItem"



interface InvoiceLineItemProps
{
    invoiceLineItem: InvoiceLineItem;
}

export default function InvoiceLineItemCard({ invoiceLineItem }: InvoiceLineItemProps)
{
    return (

        <div>
            <p><strong>${invoiceLineItem.amount.toFixed(2)}</strong></p>
            <p>{invoiceLineItem.description ?? 'No descriptrion'}</p>
        </div>


    )
}
