import LayoutDefault from "../../src/layout/LayoutDefault/LayoutDefault";
import Home from "../../src/pages/Home/Home";
import Login from "../Components/Authentication/Login/Login";
import Register from "../Components/Authentication/Register/Register";
import CategoryPage from "../pages/CategoryPages/CategoryPage";
import SubCategoryPage from "../pages/CategoryPages/SubCategoryPage";
import PrivateRouter from "../Components/PrivateRouters/PrivateRouter";
import InforUser from "../Components/InforUser/InforUser";
import PublicRoute from "../Components/PublicRouter/PublicRouter";

export const routes = [
  {
    path: "/",
    element: <LayoutDefault />,
    children: [
      {
        path: "/",
        element: <Home />,
      },
      {
        path: ":categorySeoKeyword",
        element: <CategoryPage />,
      },
      {
        path: ":categorySeoKeyword/:subCategorySeoKeyword",
        element: <SubCategoryPage />,
      },
      {
        path: "/login",
        element: (
          <PublicRoute>
            <Login />
          </PublicRoute>
        ),
      },
      {
        path: "/register",
        element: (
          <PublicRoute>
            <Register />
          </PublicRoute>
        ),
      },
      {
        path: "inforuser",
        element: (
          <PrivateRouter>
            <InforUser />
          </PrivateRouter>
        ),
      },
    ],
  },
];
