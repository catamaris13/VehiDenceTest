
import './App.css';
import {BrowserRouter as Router,Routes,Route} from 'react-router-dom';
import Registration from './components/Registration';
import Login from './components/Login';


function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
         <Route path='/' element={<Login/>}/>
         <Route path='/registration' element={<Registration/>}/>
         
        </Routes>
      </Router>
    </div>
  );
}

export default App;
