import { Link } from "react-router-dom";
import { useState, useEffect } from "react";

const Home = () => {
  const storedLogin = localStorage.getItem("islogin");
  const initialLogin = storedLogin ? JSON.parse(storedLogin) : false;
  const [login, setLogin] = useState(initialLogin);
  const [destination, setDestination] = useState("/login");

  useEffect(() => {
    if (login) {
      setDestination("/myaccount");
    } else {
      setDestination("/login");
    }
  }, [login]); 

  return (
    <div className="home">
      <nav className="navbar">
        <h1>Vehi Dence</h1>
        <div className="links">
          <Link
            to={destination}
            style={{
              color: "white",
              backgroundColor: "#3c009d",
              borderRadius: "10px",
              padding: "8px 20px",
            }}
          >
            My account
          </Link>
        </div>
      </nav>
    </div>
  );
};
export default Home;
