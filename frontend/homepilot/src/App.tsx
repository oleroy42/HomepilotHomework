import { useEffect, useState } from 'react';
import axios from 'axios';

function App() {
  const [hello, setHello] = useState<string | null >(null);

  useEffect(() => {
    axios.get('https://localhost:7067/amendments/hello')
      .then(res => {
            setHello(res.data);
        }
      )
  }, );

  return (<div>{!!hello ? hello : 'loading' }</div>);
  
}

export default App;
