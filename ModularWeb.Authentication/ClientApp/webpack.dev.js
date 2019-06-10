const path = require('path');
const webpackMerge = require('webpack-merge');
const commonConfig = require('./webpack.common.js');

module.exports = webpackMerge(commonConfig, {
  mode: 'development',
  devtool: 'cheap-module-source-map',
  output: {
    publicPath: '/',
    path: path.resolve(__dirname, '../wwwroot'),
    filename: '[name].[hash].js'
  }
});