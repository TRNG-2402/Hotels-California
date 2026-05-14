import { useState } from "react"
import type { Invoice } from "../types/Invoice";


export default function Invoice() 
{
    const [invoiceList, setInvoiceList] = useState<Invoice[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);



    return (
        <div>Invoice</div>
    )
}
