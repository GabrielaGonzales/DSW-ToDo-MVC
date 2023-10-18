using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Models;

namespace ToDoList.Services;

public class AssignmentService {
  private readonly IMongoCollection<Assignment> _assignmentsCollection;

  public AssignmentService(IOptions<MongoDBSettings> assignmentDatabaseSettings) {
    var mongoClient = new MongoClient(assignmentDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(assignmentDatabaseSettings.Value.DatabaseName);
    _assignmentsCollection = mongoDatabase.GetCollection<Assignment>(assignmentDatabaseSettings.Value.CollectionName);
  }

  public async Task<List<Assignment>> GetAsync() =>
    await _assignmentsCollection.Find(_ => true).ToListAsync();

  public async Task<Assignment?> GetAsync(string id) =>
    await _assignmentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

  public async Task CreateAsync(Assignment newAssignment) =>
    await _assignmentsCollection.InsertOneAsync(newAssignment);

  public async Task UpdateAsync(string id, Assignment updatedAssignment) =>
    await _assignmentsCollection.ReplaceOneAsync(x => x.Id == id, updatedAssignment);

  public async Task RemoveAsync(string id) =>
    await _assignmentsCollection.DeleteOneAsync(x => x.Id == id);
}