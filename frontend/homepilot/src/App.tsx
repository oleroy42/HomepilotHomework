import { useEffect, useState } from 'react';
import axios from 'axios';
import AmendmentsTableLoaded from './AmendmentsTableLoaded.tsx';
import AddAmendmentForm from './AddAmendment/AddAmendmentModal.tsx';
import React from 'react';

export interface Lease {
  id: string;
  name: string;
  rent: number;
}

interface Tenant {
  id: string;
  firstName: string;
  lastName: string;
}

export interface Amendment {
  id: string;
  lease: Lease;
  effectiveDate: string;
  entries: Tenant[];
  exits: Tenant[];
  oldRent: number | null;
}

function App() {
  const [amendments, setAmendments] = useState<Amendment[] | null >(null);
  const [showAddAmendment, setShowAddAmendment] = useState<boolean>(false);
  useEffect(() => {
    if (!amendments){
      axios.get('https://localhost:7067/amendments')
        .then(res => {
              setAmendments(res.data);
          }
        );
    }
  }, [amendments]);

  return (<div>
          {!!amendments 
            ? <AmendmentsTableLoaded amendments={amendments}/>
            : 'loading' }
            <button className='button' onClick={() => setShowAddAmendment(true)}>Add amendment</button>
            {
              showAddAmendment && 
                <AddAmendmentForm closeModal={() => setShowAddAmendment(false)} 
                                  setNewAmendment={(a) => setAmendments([...amendments!, a])}/>
            }
          </div>);
  
}

export default App;
