import axios from "axios";
import React, { useEffect, useState } from "react";
import { Amendment, Lease } from "../App";

const addAmendment = (
        entries: string | null,
        exits: string | null,
        newRent: number | null,
        selectedLease: Lease,
        closeModal: () => void,
        setNewAmendment: (amendment: Amendment) => void
) => {
    axios.post<string>('https://localhost:7067/amendments/add',
    {
        leaseId: selectedLease.id,
        entries: entries?.split(',')
                         .map(s => {return {firstName: s.split(' ')[0],
                                   lastName: s.split(' ')[1]}}),
        exits: exits?.split(',')
                        .map(s => {return {firstName: s.split(' ')[0],
                                   lastName: s.split(' ')[1]}}),
        newRent
    }
        )
        .then((newId) => {
            setNewAmendment(
                {
                    id: newId.data,
                    effectiveDate: (new Date()).toISOString(),
                    lease: selectedLease, //TODO: rent
                    oldRent: null, //TODO
                    entries: entries?.split(',')
                        .map(s => {return {
                              id: 'TODO',  
                              firstName: s.split(' ')[0],
                              lastName: s.split(' ')[1]}}) ?? [],
                    exits: exits?.split(',')
                        .map(s => {return {
                                    id: 'TODO',
                                    firstName: s.split(' ')[0],
                                    lastName: s.split(' ')[1]}}) ?? []                                      
                }
            );
            closeModal();
        });
   
}

const AddAmendmentForm = (
    {
        closeModal,
        setNewAmendment
    }: {
        closeModal: () => void;
        setNewAmendment: (amendment: Amendment) => void
    }
) => {
    const [entries, setEntries] = useState<string | null>(null);
    const [exits, setExits] = useState<string | null>(null);
    const [newRent, setNewRent] = useState<number | null>(null);
    const [leases, setLeases] = useState<Lease[] | null >(null);
    const [selectedLease, setSelectedLease] = useState<string | null>(null);

    useEffect(() => {
        if (!leases){
          axios.get<Lease[]>('https://localhost:7067/lease/active')
            .then(res => {
                  setLeases(res.data);
                  setSelectedLease(res.data[0].id);
              }
            );
        }
      }, [leases]);

    return (
        <div>
            {
                leases 
                    ? <select name="pets" id="pet-select" onChange={(e) => setSelectedLease(e.target.value)}>
                            {leases.map(l => <option value={l.id}>{l.name}</option>) }                            
                        </select>
                    : <div>loading leases</div>
            }
            <div>new tenants</div>
            <input className='input' type="text" placeholder='new tenants' onChange={(e) => setEntries(e.currentTarget.value)}></input>
            <div>leaving tenants</div>
            <input className='input' type="text" placeholder='leaving tenants' onChange={(e) => setExits(e.currentTarget.value)}></input>
            <div>new rent</div>
            <input className='input' type="text" placeholder='new Rent' onChange={(e) => setNewRent(parseInt(e.currentTarget.value))}></input>

            <button className='button'
                    onClick={() => addAmendment(entries,
                                                exits,
                                                newRent,
                                                leases!.filter(l => l.id == selectedLease!)[0],
                                                closeModal,
                                                setNewAmendment)}
                    >
                    Add amendment
            </button>
        </div>
    );
}

export default AddAmendmentForm;