import { useState, useEffect } from "react";
import { getAllUser } from "../../services/userService";

function InforUser() {
  const [listUser, setListUser] = useState([]);

  useEffect(() => {
    const fetchAllUser = async () => {
      try {
        const data = await getAllUser();
        setListUser(data);
      } catch (err) {
        console.error("Error fetching users:", err);
      }
    };

    fetchAllUser();
  }, []);

  console.log(listUser)

  return (
    <div>
      <h2>inforuser</h2>
    </div>
  );
}

export default InforUser;
