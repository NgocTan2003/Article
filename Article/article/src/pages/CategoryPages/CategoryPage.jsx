import { Outlet, useParams } from "react-router-dom";

function CategoryDetail() {
  const { categorySeoKeyword } = useParams();

  return (
    <div>
      <h1>Category: {categorySeoKeyword}</h1>
      {/* <Outlet /> */}
    </div>
  );
}

export default CategoryDetail;
