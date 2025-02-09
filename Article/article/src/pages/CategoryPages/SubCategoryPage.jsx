import { useParams } from "react-router-dom";

function SubCategoryDetail() {
  const { categorySeoKeyword, subCategorySeoKeyword } = useParams();


  return (
    <div>
      Category: {categorySeoKeyword} - SubCategory: {subCategorySeoKeyword}
    </div>
  );
}

export default SubCategoryDetail;
