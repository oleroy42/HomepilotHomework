import { Amendment } from "./App";

const AmendmentsTableLoaded = (
    {
        amendments
    }: {
        amendments: Amendment[];
    }
) => {

    return (
        <table className='table'>
            <thead>
                <tr>
                <th>Nom du bail</th>
                <th>Date de prise d'effet</th>
                <th>Nouveaux locataires</th>
                <th>Locataires sortants</th>
                <th>Nouveau loyer</th>
                </tr>
            </thead>
            <tbody>
                {amendments.map(a => 
                    <tr key={a.id}>
                    <td>{a.lease.name}</td>
                    <td>{(new Date(a.effectiveDate)).toLocaleDateString('fr-FR')}</td>
                    <td>{a.entries.map(e => e.firstName + ' ' + e.lastName).join(',')}</td>
                    <td>{a.exits.map(e => e.firstName + ' ' + e.lastName).join(',')}</td>
                    <td>{ !! a.oldRent ?  new Intl.NumberFormat('fr-FR', { style: 'currency', currency: 'EUR' }).format(a.lease.rent / 100) : null}</td>
                    </tr>
                    )}
                
            </tbody>
        </table>    

    );
}

export default AmendmentsTableLoaded;