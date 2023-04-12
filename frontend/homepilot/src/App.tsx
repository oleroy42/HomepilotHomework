import { useEffect, useState } from 'react';
import axios from 'axios';
import AmendmentTableLoaded from './AmendmentTableLoaded.tsx';

interface Lease {
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
  lease: Lease;
  effectiveDate: string;
  entries: Tenant[];
  exits: Tenant[];
  oldRent: number;
}

function App() {
  const [amendments, setAmendments] = useState<Amendment[] | null >(null);

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
            ? <AmendmentTableLoaded amendments={amendments}/>
            : 'loading' }
          </div>);
  
}

export default App;
