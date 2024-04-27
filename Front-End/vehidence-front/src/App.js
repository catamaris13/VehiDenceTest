import Navbar from "./Navbar";
import Home from "./Home";
import Login from "./LoginSignUp/Login";
import { useEffect, useState } from "react";

const App = () => {
  const currentPath = window.location.pathname;
  if (currentPath == "/login") {
    return <Login />;
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

export default App;
