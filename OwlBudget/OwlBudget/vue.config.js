module.exports = {
  lintOnSave: false,
  outputDir: 'wwwroot',

  devServer: {
    proxy: 'http://localhost:5001/',
    overlay: {
      warnings: true,
      errors: true,
    },
  },

  runtimeCompiler: true,
};
