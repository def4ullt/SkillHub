using Domain.Entities;
using Domain.ValueObjects;
using MongoDB.Driver;

namespace Infrastructure.Context.FakeData
{
    public class TaskReviewSeeder : IDataSeeder
    {
        private readonly IMongoCollection<TaskReview> _reviews;

        public TaskReviewSeeder(MongoDbContext context)
        {
            _reviews = context.TaskReviews;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            var count = await _reviews.CountDocumentsAsync(FilterDefinition<TaskReview>.Empty, cancellationToken: cancellationToken);
            if (count >= 86) return;
            if (count > 0)
                await _reviews.DeleteManyAsync(FilterDefinition<TaskReview>.Empty, cancellationToken: cancellationToken);

            var taskTesting   = Guid.Parse("b3e52d67-aef5-443e-8023-eaa42ddc2b3a");
            var taskSensor    = Guid.Parse("ec6ddafc-3424-4131-b14c-dbb2a8c69327");
            var taskBandwidth = Guid.Parse("32e261e0-c061-4d36-967c-52e3b7fa17a6");
            var taskSystem    = Guid.Parse("f0b25a52-0a98-468d-aa5c-2ea93f4f5667");
            var taskProgram   = Guid.Parse("dd6d50e5-4b28-4e69-8ab1-9dc621a347d5");
            var taskBus       = Guid.Parse("2ff5acd4-5a25-453a-86ae-52c342c9e7e7");
            var taskArray     = Guid.Parse("d4e232b0-7ff9-4451-a21c-c549ae2e06f2");
            var taskMatrix    = Guid.Parse("7ff5f10b-678d-4182-9fb5-6826479fc887");

            var fakeData = new List<TaskReview>
            {
                // Testing task (8 reviews)
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Alice", "King"), 5,
                    "Amazing task! Learned a ton about async patterns. The examples were crystal clear and very practical."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Bob", "Marsh"), 4,
                    "Pretty solid. Wish there were more edge cases to handle, but overall a great learning experience."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Leo", "Parker"), 2,
                    "The description was confusing and requirements were vague. Lost a lot of time debugging unclear instructions."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Ella", "Stone"), 5,
                    "Super useful! Practical real-world scenario that I could immediately apply at work. Highly recommend."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Ryan", "Foster"), 3,
                    "Decent task but nothing groundbreaking. The difficulty rating seems a bit off compared to the actual challenge."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Chloe", "Adams"), 5,
                    "Excellent work from the task author. Clear goals, good examples, and the feedback loop was very helpful."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Nathan", "Cruz"), 1,
                    "Terrible experience. Broken test cases and no error messages. Would not recommend to anyone."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Mia", "Johnson"), 4,
                    "Really enjoyed working through this. The structure was logical and I learned something new."),

                // Input sensor task (7 reviews)
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Mark", "Davis"), 3,
                    "Average difficulty, nothing special. The task was okay but felt repetitive compared to others."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Sofia", "Brown"), 1,
                    "Absolutely frustrating. Missing dependencies, broken setup, and no helpful guidance whatsoever."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "James", "Wilson"), 4,
                    "Good challenge! Had to dig into the docs, which was actually great for learning new concepts."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Nina", "Taylor"), 5,
                    "Best task in the module. Clear goals, realistic scenario, and the XP reward feels well-earned."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Derek", "Hall"), 2,
                    "Poorly designed task. Instructions were outdated and the example code had bugs in it."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Lily", "Chen"), 5,
                    "Brilliant! This task really pushed me to think differently about sensor data handling. Loved it."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Oscar", "Price"), 4,
                    "Well structured with clear milestones. Great for beginners and helpful for more experienced devs too."),

                // Transmit bandwidth task (8 reviews)
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Tom", "Anderson"), 3,
                    "Not bad, not great. Does the job. Instructions could be more detailed for beginners getting started."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Eva", "Martin"), 2,
                    "Too easy for the difficulty label. Expected something more challenging for the listed skill level."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Chris", "Lee"), 5,
                    "Excellent task! Combines theory with practical implementation. Really deepened my understanding of networking."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Hannah", "White"), 4,
                    "Very well thought out. The progression from simple to complex kept me engaged throughout the whole process."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Jack", "Moore"), 1,
                    "Confusing and poorly explained. The requirements contradicted each other and support was non-existent."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Grace", "Clark"), 5,
                    "Outstanding! Loved the real-world application. This is exactly the kind of practical experience I needed."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Ethan", "Young"), 4,
                    "Solid task with good learning outcomes. Could use a few more hints for the tricky parts though."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Zoe", "Walker"), 3,
                    "Okay overall. The bandwidth simulation was interesting but the setup instructions were a bit rough."),

                // Transmit system task (9 reviews)
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Liam", "Harris"), 5,
                    "Absolutely loved this task. The system design challenge was realistic and educational at the same time."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Ava", "Scott"), 4,
                    "Great learning opportunity. Pushed me to research distributed systems concepts I hadn't touched before."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Noah", "Lewis"), 2,
                    "Frustrating task. The requirements were unclear and the grading criteria were never explained properly."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Emma", "Green"), 5,
                    "Perfect difficulty level! Challenging enough to be rewarding but not so hard it becomes discouraging."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Aiden", "Baker"), 3,
                    "Decent task. Some parts were interesting but others felt like busy work with no clear educational value."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Isabella", "Nelson"), 5,
                    "Top notch task design. Every part had a purpose and the final integration test was very satisfying."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Lucas", "Carter"), 4,
                    "Really enjoyable. The mentor feedback helped a lot and the task itself was well worth the time invested."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Sophia", "Mitchell"), 1,
                    "Terrible. Zero documentation, broken starter code, and the deadline was unrealistic for the scope."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Mason", "Roberts"), 4,
                    "Good challenge! I learned a lot about message passing and system reliability through this task."),

                // Parse program task (7 reviews)
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Olivia", "Turner"), 5,
                    "This task was a game changer for me. Parsing logic is something I always avoided, not anymore!"),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Elijah", "Phillips"), 2,
                    "Overly complex without enough guidance. The example parser didn't match the actual task requirements."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Charlotte", "Evans"), 4,
                    "Challenging but fair. Good progression and the test cases were comprehensive and well organized."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Benjamin", "Edwards"), 5,
                    "I loved how this task made me think about grammar and tokenization. Excellent learning experience."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Amelia", "Collins"), 3,
                    "Mixed feelings. The first half was great but the second half felt rushed and underdeveloped."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "William", "Stewart"), 4,
                    "Really good task overall. Would recommend to anyone wanting to strengthen their parsing skills."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Harper", "Morris"), 1,
                    "Waste of time. The task description was ambiguous and the automated grader had obvious bugs in it."),

                // Quantify bus task (8 reviews)
                new(taskBus, new UserInformation(Guid.NewGuid(), "Evelyn", "Rogers"), 5,
                    "Brilliant task! Bus quantification is complex and this explained it in a very approachable way."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Alexander", "Reed"), 3,
                    "Average. Nothing terrible, nothing great. Could benefit from more context on why this matters."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Abigail", "Cook"), 4,
                    "Well designed with clear success criteria. Nice to see a task that respects the student's time."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Daniel", "Morgan"), 2,
                    "Confusing task structure. The hints were misleading and I ended up going in completely wrong direction."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Emily", "Bell"), 5,
                    "Excellent! Real-world problem with a satisfying solution. I gained practical skills I can use immediately."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Michael", "Murphy"), 4,
                    "Good task with meaningful outcomes. The progression made sense and the difficulty ramp-up was smooth."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Elizabeth", "Bailey"), 1,
                    "Very disappointing. The task seemed abandoned halfway through — key resources were missing or broken."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Matthew", "Rivera"), 5,
                    "Amazing experience! Learned about bus protocols in a way that finally made everything click for me."),

                // Compress array task (7 reviews)
                new(taskArray, new UserInformation(Guid.NewGuid(), "Scarlett", "Cooper"), 5,
                    "Loved this challenge! Array compression algorithms are essential and this task taught them brilliantly."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Henry", "Richardson"), 4,
                    "Great task that balanced theory and practice well. The bonus challenge was especially rewarding."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Victoria", "Cox"), 2,
                    "Too much assumed knowledge. Without background in compression, this task is nearly impossible to start."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Sebastian", "Howard"), 5,
                    "One of the best tasks I've done here. Very clear, very practical, and the XP reward was well deserved."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Aria", "Ward"), 3,
                    "Okay task. The compression part was interesting but setup took way too long for such a simple exercise."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Jack", "Torres"), 4,
                    "Really informative. I now understand why efficient memory usage matters and how to achieve it properly."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Penelope", "Peterson"), 1,
                    "Frustrating and poorly documented. I spent most of the time fighting the environment, not learning."),

                // Reboot matrix task (8 reviews)
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Luna", "Gray"), 5,
                    "This task was fantastic! Matrix operations in a real reboot scenario — super creative and educational."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Wyatt", "Ramirez"), 4,
                    "Solid task. The matrix manipulation problems were well thought out and the difficulty felt just right."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Nora", "James"), 2,
                    "Disappointing. Felt more like a math exam than a coding task. Needs better real-world context."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Julian", "Watson"), 5,
                    "Absolutely love this kind of task. Applied linear algebra in a way that actually made sense to me."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Layla", "Brooks"), 3,
                    "Average. Some parts were engaging but the reboot simulation felt disconnected from the matrix exercises."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Grayson", "Kelly"), 4,
                    "Good overall. Learned new matrix algorithms I wasn't familiar with. Would recommend to others."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Riley", "Sanders"), 5,
                    "Perfect task for anyone who wants to understand state machines combined with linear algebra. Excellent!"),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Ellie", "Price"), 1,
                    "The task description was copy-pasted from somewhere and made no sense. Very poor quality overall."),

                // Testing task — extra reviews (3)
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Finn", "Walsh"), 5,
                    "Phenomenal task. The async testing patterns covered here are directly applicable to real production code."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Isla", "Hunter"), 2,
                    "Instructions lacked depth. The starter code had issues that wasted hours before I could even begin properly."),
                new(taskTesting, new UserInformation(Guid.NewGuid(), "Theo", "Grant"), 4,
                    "Really good structure and clear acceptance criteria. Learned solid unit testing skills from this challenge."),

                // Input sensor task — extra reviews (3)
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Piper", "Nash"), 5,
                    "Outstanding task design. The sensor simulation felt realistic and the edge cases were genuinely educational."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Jasper", "Flynn"), 3,
                    "Decent but not memorable. The feedback loop between input and output could be more interactive."),
                new(taskSensor, new UserInformation(Guid.NewGuid(), "Freya", "Shaw"), 4,
                    "Well balanced difficulty. Good documentation and the expected outputs were clearly defined throughout."),

                // Transmit bandwidth task — extra reviews (3)
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Rory", "Blake"), 5,
                    "Excellent practical task. Bandwidth throttling concepts clicked for me after completing this challenge."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Nadia", "Stone"), 1,
                    "Poorly maintained. The test runner was broken on Windows and there was no workaround in the docs."),
                new(taskBandwidth, new UserInformation(Guid.NewGuid(), "Miles", "Ford"), 4,
                    "Solid networking task. The simulation was realistic and the grading rubric was fair and transparent."),

                // Transmit system task — extra reviews (3)
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Paige", "Warren"), 5,
                    "Best distributed systems task I've done. Realistic constraints and very well paced progression overall."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Felix", "Lane"), 2,
                    "Too much ambiguity in the requirements. Spent most of my time guessing the expected behavior rather than coding."),
                new(taskSystem, new UserInformation(Guid.NewGuid(), "Zara", "Cross"), 4,
                    "Genuinely challenging and educational. The system design component pushed me to think at a higher level."),

                // Parse program task — extra reviews (3)
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Hugo", "Marsh"), 5,
                    "This completely changed how I think about parsers. The incremental approach made a complex topic approachable."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Ivy", "Cole"), 2,
                    "The grammar specification was incomplete. Had to reverse-engineer expected behavior from failing test cases."),
                new(taskProgram, new UserInformation(Guid.NewGuid(), "Ezra", "Banks"), 4,
                    "Really well thought out progression. Tokenization to full AST walked through clearly with helpful examples."),

                // Quantify bus task — extra reviews (3)
                new(taskBus, new UserInformation(Guid.NewGuid(), "Clara", "Webb"), 5,
                    "Perfect blend of theory and practice. Bus quantification is tricky but this task made it genuinely fun."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Otto", "Marsh"), 3,
                    "Okay task. The quantification model was interesting but setup steps were unnecessarily complex to follow."),
                new(taskBus, new UserInformation(Guid.NewGuid(), "Vera", "Quinn"), 4,
                    "Good challenge with clear goals. The real-world bus protocol context made the abstract concepts concrete."),

                // Compress array task — extra reviews (3)
                new(taskArray, new UserInformation(Guid.NewGuid(), "Rex", "Dunn"), 5,
                    "Loved this! Implementing compression algorithms from scratch really deepened my understanding of memory management."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Lena", "Cross"), 1,
                    "Terrible documentation. Spent three hours on setup alone and the expected outputs were never clearly defined."),
                new(taskArray, new UserInformation(Guid.NewGuid(), "Sven", "Hart"), 4,
                    "Excellent hands-on task. The compression benchmarks at the end showed the real impact of the work."),

                // Reboot matrix task — extra reviews (3)
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Petra", "Stone"), 5,
                    "Brilliantly designed. The matrix reboot scenario tied together state machines and linear algebra beautifully."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Axel", "Reid"), 2,
                    "Interesting concept but poorly executed. The matrix size constraints were never explained in the task spec."),
                new(taskMatrix, new UserInformation(Guid.NewGuid(), "Mika", "Sato"), 4,
                    "Solid challenge. The progression from simple matrix ops to full reboot simulation was well paced throughout."),
            };

            foreach (var review in fakeData)
            {
                var (sentiment, score) = GetSentiment(review.Rating);
                review.SetSentiment(sentiment, GetKeywords(review.Comment));
            }

            await _reviews.InsertManyAsync(fakeData, cancellationToken: cancellationToken);
        }

        private static (string sentiment, int score) GetSentiment(int rating) => rating switch
        {
            >= 4 => ("Positive", 1),
            3 => ("Neutral", 0),
            _ => ("Negative", -1)
        };

        private static List<string> GetKeywords(string comment)
        {
            var stopwords = new HashSet<string> { "the", "a", "an", "and", "or", "but", "in", "on", "at", "to", "for",
                "of", "with", "is", "was", "were", "be", "been", "have", "has", "had", "it", "its", "not", "this", "that" };
            return comment.ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > 3 && !stopwords.Contains(w))
                .Take(5)
                .ToList();
        }
    }
}
