import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import "./home.css"; // Import the CSS file
import ferrariImage from "./assets/ferrari copy.jpg"; // Import the image file
import mercedesImage from "./assets/mercedes.jpg";
import bmwImage from "./assets/Bmw.jpg";
import audiImage from "./assets/Audi.jpg";

const Home = () => {
  const [masini, setMasini] = useState([]);
  const [login, setLogin] = useState(localStorage.getItem("islogin") ? JSON.parse(localStorage.getItem("islogin")) : false);
  const [destination, setDestination] = useState("/login");
  const [displayHorizontal, setDisplayHorizontal] = useState(true);

  const [name, setName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phone, setPhone] = useState("");
  const [token, setToken] = useState("");
  const emailUser = localStorage.getItem("email");

  useEffect(() => {
    axios
      .get("http://localhost:5277/api/User/All/Users")
      .then((response) => {
        const userData = response.data;
        if (userData.length > 0) {
          const user = userData.find((user) => user.email === emailUser);
          if (user) {
            setName(user.name);
            setUsername(user.username);
            setEmail(user.email);
            setPassword(user.password);
            setPhone(user.phoneNo);
          }
        }
      })
      .catch((error) => {
        console.error("Error fetching user data:", error);
      });
  }, []);

  useEffect(() => {
    if (login) {
      setDestination("/myaccount");
    } else {
      setDestination("/login");
    }
  }, [login]);

  useEffect(() => {
    const handleResize = () => {
      setDisplayHorizontal(window.innerWidth > 768);
    };

    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(`http://localhost:5277/api/Masina/MasinaList/${username}`);
        setMasini(response.data.listMasina);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };
  
    fetchData();
  }, [username]);
  

  // Mapam fiecare masina la un card
  const mapMasiniToCards = () => {

    if(!masini){
      return(
        <h1 className="text">Nu aveti masini...</h1>
      );
    }

    return masini.map((masina, index) => {
      let imageSrc;
      switch (masina.marca.toLowerCase()) {
        case "ferrari":
          imageSrc = ferrariImage;
          break;
        case "mercedes-benz":
          imageSrc = mercedesImage;
          break;
        case "bmw":
          imageSrc = bmwImage;
          break;
        case "audi":
          imageSrc = audiImage;
          break;
        default:
          imageSrc = "";
          break;
      }
      return (
        <div className="card" key={index}>
          <img src={imageSrc} className="card-img-top" alt={masina.marca} />
          <div className="card-body">
            <h5 className="card-title">{masina.marca} {masina.model}</h5>
            <p className="card-text">Nr. Inmatriculare: {masina.nrInmatriculare}</p>
            <p className="card-text">Serie Sasiu: {masina.serieSasiu}</p>
          </div>
        </div>
      );
    });
  };

  if (!login) {
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
              marginLeft: "20px",
            }}
          >
            My account
          </Link>
        </div>
      </nav>
    </div>
    ); 
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
              marginLeft: "20px",
            }}
          >
            My account
          </Link>
        </div>
      </nav>

      <div className={`card-container ${displayHorizontal ? "" : "vertical"}`}>
        {mapMasiniToCards()}
      </div>
    </div>
  );
};

export default Home;
