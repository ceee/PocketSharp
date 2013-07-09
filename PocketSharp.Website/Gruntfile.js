

module.exports = function(grunt)
{
	'use strict';

	// configuration for grunt
	grunt.initConfig({

		// loads the package configuration file
		pkg: grunt.file.readJSON('package.json'),


		// concatenate files
		concat: {
			debug: {
				src: [ 'Assets/Javascripts/**/*.js' ],
				dest: 'Debug/app.js'
			}
		},


		// uglifier options
		uglify: {
			options: {
				banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
			},
			debug: {
				src: '<%= concat.debug.src %>',
				dest: 'Release/app.js'
			}
		},


		// compile compass files
		compass: {
			debug: {
				options: {
					sassDir: 'Assets/Stylesheets',
					cssDir: 'Debug',
					imagesDir: 'Assets/Images',
					fontDir: 'Assets/Fonts',
					javascriptsDir: 'Assets/Javascripts',
					outputStyle: 'expanded',
					environment: 'development',
					require: [
						'zurb-foundation'
					]
				}
			},
			release: {
				options: {
					sassDir: 'Assets/Stylesheets',
					cssDir: 'Release',
					imagesDir: 'Assets/Images',
					fontDir: 'Assets/Fonts',
					javascriptsDir: 'Assets/Javascripts',
					outputStyle: 'compressed',
					environment: 'production',
					require: [
						'zurb-foundation'
					]
				}
			}
		},


		// hint all the JS files
		jshint: {
			files: [],
			options: {
				globals: {
					jQuery: true,
					console: true,
					module: true,
					document: true
				}
			}
		},


		// image optimization for dist
		imagemin: {
			release: {
				options: {
					optimizationLevel: 3
				},
				files: []
			}
		},


		// watch file changes to automate tasks
		watch: {
			js: {
				files: [ '<%= concat.debug.src %>' ],
				tasks: [ 'concat' ]
			},
			sass: {
				files: [ '<%= compass.debug.options.sassDir %>/**/*.scss' ],
				tasks: [ 'compass:debug' ]
			}
		}
	});


	// load tasks
	grunt.loadNpmTasks('grunt-contrib-uglify');
	grunt.loadNpmTasks('grunt-contrib-concat');
	grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-compass');
	grunt.loadNpmTasks('grunt-contrib-imagemin');
	grunt.loadNpmTasks('grunt-contrib-handlebars');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-datauri');


	// build task, which generates the development JS
	grunt.registerTask('debug', [ 
		'concat', 
		'compass:debug'
	]);

	// distribution task
	grunt.registerTask('release', [ 
		'concat',
		'uglify', 
		'compass:release' 
	]);

};
