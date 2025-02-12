import logo from './logo.svg';
import './App.css';
import AddForm from './AddForm';
import Grid from './Grid';
import UpdateForm from './UpdateForm';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        
        
        <Grid/>
        <AddForm />
        
        <UpdateForm />
        
        
        
      </header>
    </div>
  );
}

export default App;
