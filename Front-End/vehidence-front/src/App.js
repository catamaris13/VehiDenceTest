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

import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from "./Home";
import Login from "./LoginSignUp/Login";
import SignUp from "./LoginSignUp/SignUp";
import EmailValidation from './EmailValidation/EmailValidation';
import MyAccount from './MyAccount/MyAccount';
import RessetPassword from './RessetPassword/RessetPassword'
import React from 'react';
import VerifyEmailPage from './RessetPassword/VerifyEmailPage';
import EnterEmailPage from './RessetPassword/EnterEmailPage';

function App() {

  return (
    <div className="App">
        <Router>
          <Routes>
            <Route path='/login' element={<Login />} />
            <Route path='/signup' element={<SignUp />} />
            <Route path='/email_validation' element={<EmailValidation />} />
            <Route path='/reset_password' element={<RessetPassword />} />
            <Route path='/myaccount' element={<MyAccount />} />
            <Route path="/enter_email" element={<EnterEmailPage/>}/>
            <Route path='/verify_email_lost_password' element={<VerifyEmailPage/>}/>
            <Route path='*' element={<Home />} />
          </Routes>
        </Router>
    </div>
  );
}

export default App;
