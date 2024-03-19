const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require("copy-webpack-plugin");

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
            template: path.resolve(__dirname, './src/pages', 'index.html'),
            filename: 'index.html',
        }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, './src/pages', 'rozklad.html'),
            filename: 'rozklad.html',
        }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, './src/pages', 'photo.html'),
            filename: 'photo.html',
        }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, './src/pages', 'news.html'),
            filename: 'news.html',
        }),
        new CopyPlugin({
            patterns: [
                {from: path.resolve(__dirname, './src/assets/images'), to: path.resolve(__dirname, './dist/assets/images')}
            ]
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
