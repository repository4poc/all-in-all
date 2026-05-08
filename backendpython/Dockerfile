FROM python:3.11-slim

# Set environment variables to prevent Python from writing .pyc files and to ensure that output is sent straight away
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Set the working directory in the container
WORKDIR /app

# Install dependencies
COPY requirements.txt .

# Upgrade pip and install the required Python packages
RUN pip install --no-cache-dir --upgrade pip \
    && pip install --no-cache-dir -r requirements.txt

# Copy the application code to the container
COPY app.py .

# Expose the port that the Flask app will run on
EXPOSE 5000

# Start the Flask application
CMD ["python", "app.py"]