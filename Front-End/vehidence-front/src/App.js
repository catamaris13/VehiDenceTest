/*import Navbar from "./Navbar";
import Home from "./Home";
import Login from "./LoginSignUp/Login";
import SignUp from "./LoginSignUp/SignUp"
import { useEffect, useState } from "react";

const App = () => {
  const currentPath = window.location.pathname;
  if (currentPath == "/login") {
    return <Login />;
  }
  if(currentPath == "/signup"){
    return <SignUp/>;
  }
  else {
    return (
      <div className="App">
        <Navbar />
        <div className="content">
          <Home />
        </div>
      </div>
    );
  }
};

export default App;*/

import {BrowserRouter as Router,Routes,Route} from 'react-router-dom';
import Login from './LoginSignUp/Login';
import SignUp from './LoginSignUp/SignUp';
import Home from "./Home";

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
         <Route path='/login' element={<Login/>}/>
         <Route path='/signup' element={<SignUp/>}/>
         <Route path='*' element={<Home/>}/>

        </Routes>
      </Router>
    </div>
  );
}

export default App;
