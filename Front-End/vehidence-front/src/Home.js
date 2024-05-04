import { Link } from "react-router-dom";

const Home = () => {
  const login = localStorage.getItem('islogin');
  console.log("login value:", login);

  let destination;
  if(Boolean(login)){
    destination = '/myaccount';
  }
  else{
    destination = '/login'
  }

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
