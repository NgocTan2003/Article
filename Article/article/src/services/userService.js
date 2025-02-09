import { axiosPublic, axiosPrivate } from "../utils/axiosCustomize";

export const getAllUser = async () => {
  try {
    const res = await axiosPrivate.get("/api/ArticleAppUser/GetAllUser");
    if (res.status === 200) {
      return res.data;
    }
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
    throw err;
  }
};
