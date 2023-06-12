import React from 'react';
import {Box, Container} from "@mui/material";
import {Outlet} from "react-router-dom";

const IndexContainer = () => {
    return (
        <Box sx={{ width: "100%", height: '100vh' }}>
            <Container>
                <Box sx={{
                    minWidth: "100%", minHeight: "100vh", padding: "20px",
                    display: "flex", flexDirection: "column", justifyContent: "center", alignItems: 'center',
                }}>
                    <Outlet />
                </Box>
            </Container>
        </Box>
    );
};

export default IndexContainer;