import { useEffect, useState } from 'react';
import axios from 'axios';

function App() {
  const [hello, setHello] = useState('loading');

  useEffect(() => {
    let mounted = true;
    axios.get('https://localhost:7067/amendments/hello')
      .then(res => {
          if(mounted) {
            setHello(res.data);
          }
        }
      )
  }, );

  return (<div>{hello}</div>);
  
}

export default App;
