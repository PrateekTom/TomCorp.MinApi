pipeline {
	agent any
	stages {
		stage('Get the source code from github') {
			steps {
				git url:'https://github.com/PrateekTom/TomCorp.MinApi.git'
			}
		}
	    stage('Build Docker Image'){
		    steps{
			    script{
				    docker.build("tomcorp.minapi")
		        }
            }
	   }
	}
}