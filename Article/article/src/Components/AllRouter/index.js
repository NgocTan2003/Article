import { useRoutes } from "react-router-dom";
import { routes } from "../../routes/index"

function AllRouter() {
    const element = useRoutes(routes);
    return (
        <>
            {element}
        </>
    )
}

export default AllRouter;