import { axiosPublic } from "../utils/axiosCustomize";

export const getAllCategory = async () => {
  try {
    const res = await axiosPublic.get("/api/ArticleCategory/GetCategoriesWithSubCategories");
    if (res.statusCode === 200) {
      return res.items;
    }
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
    throw err;
  }
};
