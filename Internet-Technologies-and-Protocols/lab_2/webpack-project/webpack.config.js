const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = {
    entry: {
        main: path.resolve(__dirname, './src', 'index.js')
    },
    module: {
        rules: [
            { test: /\.css$/, use: [ 'style-loader', 'css-loader' ] }
        ]
    },
    plugins: [
        new CleanWebpackPlugin(),
        new HtmlWebpackPlugin({
            title: 'Index lab_2',
            template: path.resolve(__dirname, './src/pages', 'index.html'),
            filename: 'index.html',
        }),
        new HtmlWebpackPlugin({
            title: 'About lab_2',
            template: path.resolve(__dirname, './src/pages', 'about.html'),
            filename: 'about.html',
        })
    ],
    devServer: {
        static: {
            directory: path.join(__dirname, 'dist'),
        },
        compress: true,
        port: 9000,
    },    
}
