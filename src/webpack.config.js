/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

const path = require('path');
const webpack = require('webpack');
const chunks = require('./webpack.chunks.js');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    entry: chunks,
    watch: true,
    output: {
        path: path.resolve(__dirname, 'wwwroot'),
        filename: '[name]'
    },
    resolve: {
        modules: [
            path.resolve(__dirname, 'Assets/js'),
            'node_modules'
        ],
        alias: {
            vue: 'vue/dist/vue.js',
            jquery: 'jquery/dist/jquery.slim.js'
        }
    },
    module: {
        rules: [{
            test: /\.es6$/,
            loader: 'babel-loader',
            query: { presets: ['es2015'], cacheDirectory: true },
            include: path.resolve(__dirname, 'Assets/js')
        }, {
            test: /\.(css|scss)$/,
            loader: ExtractTextPlugin.extract({
                fallback: 'style-loader',
                use: [
                    { loader: 'css-loader', query: { minimize: true } },
                    { loader: 'sass-loader' }
                ]
            })
        }, {
            test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
            loader: 'file-loader',
            options: { name: '/fonts/[name].[ext]' }
        }]
    },
    plugins: [
        new webpack.optimize.UglifyJsPlugin({
            sourceMap: false,
            minimize: true,
            output: {
                comments: false
            }
        }),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'js/common.min.js',
            filename: '[name]',
            minChunks: Infinity
        }),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery'
        }),
        new ExtractTextPlugin('[name]', {
            allChunks: true
        }),
        new CopyWebpackPlugin([{
            from: path.resolve(__dirname, 'Assets/img'),
            to: 'img/[name].[ext]'
        }, {
            from: path.resolve(__dirname, 'Assets/fonts'),
            to: 'fonts/[name].[ext]'
        }])
    ]
};
