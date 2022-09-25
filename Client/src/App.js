import './App.css';
import img from './img.png';
import { useState } from 'react'

function App() {
  const [result, setResult] = useState(undefined);
  const [loading, setLoading] = useState(false);
  const handleSubmit = e => {
    e.preventDefault();
    console.log(e.target);
    if (!e.target.tries.value) {
      alert("No. of tries should not be empty");
    } else if (e.target.tries.value <= 0) {
      alert("No. of tries should be positive");
    }
    // console.log("switch", e.target.switch.checked);
    setLoading(true);
    fetch('http://localhost:5000/api/montyhall/play', {
      method: 'POST',
      headers: {
        'content-type': 'application/json'
      },
      body: JSON.stringify({
        tries: e.target.tries.value,
        isSwitchStrategyEnabled: e.target.switch.checked
      })
    }
    ).then(res => res.json()).then(data => {
      setResult(data.payload);
      setLoading(false);
    })
  };

  return (
    <div className="App">
      <img src={img} className="logo" alt="Business view - Reports" />
      <form className="form" onSubmit={handleSubmit}>
        <div className="input-group">
          <label htmlFor="Tries">Number of Tries</label>
          <input type="number" name="tries" placeholder="100" />
        </div>
        <div className="input-group">
          <label htmlFor="switch">Switch Door</label>
          <input type="checkbox" name="switch" />
        </div>
        <button className="primary">Simulate</button>
        <br></br>
        <center>
          {
            loading &&

            <div className='result loader'></div>
          }

          {result && !loading && < div className="result input-group">
            <label >Strategy: {result.strategy}</label>
            <label >Win Count: {result.winCount}</label>
            <label >Loss Count: {result.lossCount} </label>
            <label >Win Percentage: {result.winPercentage}%</label>
            <label >Loss Percentage: {result.lossPercentage}%</label>
          </div>}
        </center>
      </form >

    </div >

  );
}

export default App;
