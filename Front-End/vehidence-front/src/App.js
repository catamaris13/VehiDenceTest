import Navbar from './Navbar';
import Home from './Home';
import { useEffect, useState } from 'react';

function App() {
  const currentPath = window.location.pathname;
  return(
         <div className="App">
      <Navbar />
      <div className="content">
        <Home />
      </div>
    </div>
  )
 
 
}

export default App;
