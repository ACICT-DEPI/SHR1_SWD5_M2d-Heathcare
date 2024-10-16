/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.cshtml',
        './Views/Shared/**/*.cshtml',
        './wwwroot/**/*.html',
        './Scripts/**/*.js'  
    ],
    theme: {
        container: {
            center: true,
            padding: "2rem",
            screens: {
                "2xl": "1400px",
            },
        },
        extend: {
            width: {
                '22': '5.5rem',
                '56': '14rem',
            },
            colors: {
                green: {
                    400:"#A4E3C1",
                    500: "#A4E3C1", // Softer green for light theme
                    600: "#2D4B42", // Dark green for accent but still visible on light background
                },
                blue: {
                    500: "#A3C7E8", // Light blue for a fresh and calm look
                    600: "#2A3A45", // Darker blue for contrast
                },
                red: {
                    500: "#F9B2B2", // Soft red to keep it calm in light theme
                    600: "#6D3A3A", // Warmer, visible red accent
                    700: "#F76A63", // Brighter red to maintain contrast
                },
                light: {
                    200: "#F5F5F5", // Very light for a clean background
                },
                dark: {
                    200: "#E8E9E9", // Light gray for subtler accents
                    300: "#B0B3B5", // Medium gray for a neutral tone
                    400: "#B7B8BA", // Another neutral tone, slightly darker
                    500: "#D3D5D7", // Light gray for soft shadows and borders
                    600: "#B8C2C8", // Soft cool gray, keeping it subtle
                    700: "#E5E8EB", // Bright for accents and dividers
                },
            },
        },
    },
    plugins: [],
}

