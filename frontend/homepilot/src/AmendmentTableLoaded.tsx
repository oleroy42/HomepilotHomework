import { Amendment } from "./App";

const AmendmentTableLoaded = (
    {
        amendments
    }: {
        amendments: Amendment[];
    }
) => {

    return (
        <table class="table">
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
                    <tr>
                    <td>{a.lease.name}</td>
                    <td>{a.effectiveDate}</td>
                    <td>{a.entries.map(e => e.firstName + ' ' + e.lastName).join(',')}</td>
                    <td>{a.exits.map(e => e.firstName + ' ' + e.lastName).join(',')}</td>
                    <td>TODO</td>
                    </tr>
                    )}
                
            </tbody>
        </table>    

    );
}

export default AmendmentTableLoaded;