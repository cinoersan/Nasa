const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {

  // webpack will take the files from ./src/index
  entry: {
    client: ['./src/index.tsx', './src/scss/main.scss']
  },
  // and output it into /dist as bundle.js
  output: {
    path: path.join(__dirname, '/dist'),
    filename: 'bundle.[hash].js',
    publicPath: "/",
  },

  // adding .ts and .tsx to resolve.extensions will help babel look for .ts and .tsx files to transpile
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.css']
  },
  devtool: 'inline-source-map',
  devServer: {
    stats: {
      children: false, // Hide children information
      maxModules: 0 // Set the maximum number of modules to be shown
    },
    index: 'index.html',
    contentBase: path.resolve(__dirname, "dist"),
    port: 9000,
    historyApiFallback: true,
    inline: true,
    hot: true
  },
  module: {
    rules: [

      // we use babel-loader to load our jsx and tsx files
      {
        test: /\.(ts|js)x?$/,
        exclude: path.resolve(__dirname, "node_modules"),
        use: {
          loader: 'babel-loader'
        },
      },
      {
        test: /\.css$/,
        exclude: path.resolve(__dirname, "node_modules"),
        include: [path.resolve(__dirname, "node_modules/react-datepicker/dist/"), path.resolve(__dirname, "node_modules/react-select/dist/")],
        use: [MiniCssExtractPlugin.loader, 'style-loader', 'css-loader']
      },
      {
        test: /\.(sass|scss)$/,
        exclude: path.resolve(__dirname, "node_modules"),

        use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader']
      },
      {
        test: /.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
        use: [{
          loader: 'file-loader',
          options: {
            name: '[name].[ext]',
            outputPath: 'fonts/',    // where the fonts will go
            publicPath: '../fonts/'       // override the default path
          }
        }]
      },
      {
        test: /.(png|jpg|jpeg|gif)$/,
        use: [{
          loader: 'file-loader',
          options: {
            name: '[name].[ext]',
            outputPath: 'img/',    // where the fonts will go
            publicPath: '../img/'       // override the default path
          }
        }]
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './src/index.html'
    }),
    new MiniCssExtractPlugin({
      filename: 'bundle.[hash].css'
    }),
    new CleanWebpackPlugin({
      cleanOnceBeforeBuildPatterns: ['**/*', '!img/**', '!fonts/**'],
    })
  ]
};