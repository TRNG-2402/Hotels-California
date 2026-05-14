import type { InvoiceLineItem } from "../types/InvoiceLineItem"
import styles from 
import { Link } from "react-router-dom"

interface InvoiceLineItemProps
{
    invoiceLineItem: InvoiceLineItem;
}

export default function InvoiceLineItemCard({ invoiceLineItem }: InvoiceLineItemProps)
{
    return (

        <div className={styles.card}>
            <p><strong>${invoiceLineItem.amount.toFixed(2)}</strong></p>
            <p>{invoiceLineItem.description ?? 'No descriptrion'}</p>
        </div>


    )
}
